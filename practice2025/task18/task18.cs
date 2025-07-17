namespace task18;

public interface ICommand
{
    void Execute();
}

public interface ILongCommand : ICommand
{
    bool IsCompleted { get; }
}

public class HardStop(ServerThread server) : ICommand
{
    public void Execute()
    {
        if (Thread.CurrentThread != server.Thread)
            throw new InvalidOperationException("Wrong thread for HardStop");
        server.StopFast();
    }
}

public class SoftStop(ServerThread server) : ICommand
{
    public void Execute()
    {
        if (Thread.CurrentThread != server.Thread)
            throw new InvalidOperationException("Wrong thread for SoftStop");
        server.StopSlow();
    }
}

public interface IScheduler
{
    bool HasCommands { get; }
    ICommand Next();
    void Schedule(ICommand cmd);
}

public class RoundRobinScheduler : IScheduler
{
    private readonly Queue<ICommand> cmds = new();
    public bool HasCommands => cmds.Count > 0;

    public void Schedule(ICommand cmd) => cmds.Enqueue(cmd);

    public ICommand Next() => cmds.Dequeue();
    public void Clear() => cmds.Clear();
}

public class ServerThread
{
    private readonly Queue<ICommand> queue = new();
    private readonly IScheduler longCommands;
    private readonly Thread thread;
    private bool isRunning = true;
    private bool softStop  = false;
    private readonly object Lock = new();

    public Thread Thread => thread;

    public ServerThread(IScheduler? scheduler = null)
    {
        longCommands = scheduler ?? new RoundRobinScheduler();
        thread = new Thread(ProcessCommands);
        thread.Start();
    }

    public void AddCommand(ICommand cmd)
    {
        lock (Lock)
        {
            if (!isRunning || softStop)
                return;

            queue.Enqueue(cmd);
            Monitor.Pulse(Lock);
        }
    }

    private void ProcessCommands()
    {
        while (isRunning)
        {
            ICommand cmd = GetNextCommand();
            if (cmd == null) break;

            if (!isRunning) break;

            cmd.Execute();

            if (isRunning && cmd is ILongCommand longCmd && !longCmd.IsCompleted)
                longCommands.Schedule(longCmd);
        }
    }

    private ICommand? GetNextCommand()
    {
        lock (Lock)
        {
            while (true)
            {
                if (!isRunning) return null;
                if (softStop && queue.Count == 0 && !longCommands.HasCommands) return null;
                
                if (queue.Count > 0) return queue.Dequeue();
                if (longCommands.HasCommands) return longCommands.Next();
                
                Monitor.Wait(Lock);
            }
        }
    }

    internal void StopFast()
    {
        lock (Lock)
        {
            isRunning = false;
            queue.Clear();
            if (longCommands is RoundRobinScheduler scheduler)
                scheduler.Clear();
            Monitor.PulseAll(Lock);
        }
    }

    internal void StopSlow()
    {
        lock (Lock)
        {
            softStop = true;
            Monitor.Pulse(Lock);
        }
    }
}
