namespace task17tests;

using task17;
using Xunit;

public class Task17Tests
{
    private class Test(Action action) : ICommand
    {
        public void Execute() => action();
    }
    
    [Fact]
    public void SoftStop_FinishesAllCommands()
    {
        var server = new ServerThread();
        int counter = 0;
        
        server.AddCommand(new Test(() => counter++));
        server.AddCommand(new Test(() => counter++));
        server.AddCommand(new SoftStop(server));
        
        server.Thread.Join(500);
        Assert.Equal(2, counter);
    }

    [Fact]
    public void HardStop_StopsImmediately()
    {
        var server = new ServerThread();
        int counter = 0;
        
        server.AddCommand(new Test(() => 
        {
            counter++;
            Thread.Sleep(100);
        }));
        server.AddCommand(new HardStop(server));
        server.AddCommand(new Test(() => counter++));
        
        server.Thread.Join(500);
        Assert.Equal(1, counter);
    }

    [Fact]
    public void StopCommands_ThrowInWrongThread()
    {
        var server = new ServerThread();
        var hardStop = new HardStop(server);
        var softStop = new SoftStop(server);
        
        Assert.Throws<InvalidOperationException>(hardStop.Execute);
        Assert.Throws<InvalidOperationException>(softStop.Execute);
        
        server.AddCommand(new HardStop(server));
    }
}