namespace task17;

public interface ICommand
{
    void Execute();
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

public class ServerThread
{
    private readonly Queue<ICommand> queue = new();
    private readonly Thread thread;
    private bool isRunning = true;
    private bool softStop = false;
    private readonly object Lock = new();

    public Thread Thread => thread;

    public ServerThread()
    {
        thread = new Thread(ProcessCommands);
        thread.Start();
    }

    public void AddCommand(ICommand command)
    {
        lock (Lock)
        {
            if (!isRunning || softStop) 
                return;
            
            queue.Enqueue(command);
            Monitor.Pulse(Lock);
        }
    }

    private void ProcessCommands()
    {
        while (isRunning)
        {
            ICommand? command = null;
            lock (Lock)
            {
                while (queue.Count == 0 && isRunning && !softStop)
                    Monitor.Wait(Lock);
                if (queue.Count > 0)
                    command = queue.Dequeue();
                else if (softStop)
                    isRunning = false;
            }
            command?.Execute();
        }
    }

    internal void StopFast()
    {
        lock (Lock)
        {
            isRunning = false;
            Monitor.Pulse(Lock);
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