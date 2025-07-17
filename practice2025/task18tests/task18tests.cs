namespace task18tests;

using task18;
using Xunit;

public class Task18Tests
{
    private class Command(Action action) : ICommand
    {
        public void Execute() => action();
    }

    private class LongCommand(int steps) : ILongCommand
    {
        public int Counter { get; private set; }
        public bool IsCompleted => Counter >= steps;

        public void Execute()
        {
            if (Counter < steps) Counter++;
        }
    }

    [Fact]
    public void ContinuousCommand_ExecutesInSteps()
    {
        var scheduler = new RoundRobinScheduler();
        var server = new ServerThread(scheduler);
        var longCmd = new LongCommand(3);
        int counter = 0;

        server.AddCommand(longCmd);
        server.AddCommand(new Command(() => counter++));
        server.AddCommand(new SoftStop(server));

        server.Thread.Join(500);

        Assert.Equal(3, longCmd.Counter);
        Assert.Equal(1, counter);
    }

    [Fact]
    public void HardStop_StopsContinuousCommand()
    {
        var server = new ServerThread();
        var longCmd = new LongCommand(3);
        bool hardStopExecuted = false;
        bool normalCommandExecuted = false;

        server.AddCommand(longCmd);
        server.AddCommand(new Command(() => hardStopExecuted = true));
        server.AddCommand(new HardStop(server));
        server.AddCommand(new Command(() => normalCommandExecuted = true));

        server.Thread.Join(500);

        Assert.True(hardStopExecuted);
        Assert.False(normalCommandExecuted);
        Assert.Equal(1, longCmd.Counter);
    }

    [Fact]
    public void SoftStop_WaitsForContinuousCommands()
    {
        var server = new ServerThread();
        var longCmd = new LongCommand(2);

        server.AddCommand(longCmd);
        server.AddCommand(new SoftStop(server));

        server.Thread.Join(500);
        Assert.Equal(2, longCmd.Counter);
    }

    [Fact]
    public void Commands_ExecuteInRoundRobinOrder()
    {
        var scheduler = new RoundRobinScheduler();
        var server = new ServerThread(scheduler);
        var results = new List<int>();
        var longCmd1 = new LongCommand(2);
        var longCmd2 = new LongCommand(2);

        server.AddCommand(longCmd1);
        server.AddCommand(longCmd2);
        server.AddCommand(new SoftStop(server));
        server.Thread.Join(500);

        Assert.Equal(2, longCmd1.Counter);
        Assert.Equal(2, longCmd2.Counter);
    }

    [Fact]
    public void StopCommands_ThrowInWrongThread()
    {
        var server = new ServerThread();
        var hardStop = new HardStop(server);
        var softStop = new SoftStop(server);

        Assert.Throws<InvalidOperationException>(hardStop.Execute);
        Assert.Throws<InvalidOperationException>(softStop.Execute);
    }
}
