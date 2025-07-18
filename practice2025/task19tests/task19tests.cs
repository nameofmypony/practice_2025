namespace task19tests;

using task19;
using Xunit;

public class Task19Tests
{

    [Fact]
    public void TestCommand_ExecutesMultipleSteps()
    {
        var command = new TestCommand(1);
        
        command.Execute();
        Assert.Equal(1, command.counter);
        Assert.False(command.IsCompleted);
        
        command.Execute();
        Assert.Equal(2, command.counter);
        Assert.False(command.IsCompleted);
        
        command.Execute();
        Assert.Equal(3, command.counter);
        Assert.True(command.IsCompleted);
    }

    [Fact]
    public void Commands_ExecuteInRoundRobinOrder()
    {
        var scheduler = new RoundRobinScheduler();
        var server = new ServerThread(scheduler);
        var commands = new TestCommand[5];
        
        for (int i = 0; i < 5; i++)
        {
            commands[i] = new TestCommand(i);
            server.AddCommand(commands[i]);
        }
        
        server.AddCommand(new SoftStop(server));
        server.Thread.Join(500);
        
        Assert.All(commands, cmd => 
        {
            Assert.Equal(3, cmd.counter);
            Assert.True(cmd.IsCompleted);
        });
    }

    [Fact]
    public void HardStop_InterruptsExecution()
    {
        var server = new ServerThread();
        var commands = new TestCommand[5];
        
        for (int i = 0; i < 5; i++)
        {
            commands[i] = new TestCommand(i);
            server.AddCommand(commands[i]);
        }
        
        server.AddCommand(new HardStop(server));
        server.Thread.Join(500);
        
        int totalSteps = commands.Sum(c => c.counter);
        Assert.True(totalSteps < 15);
        Assert.Contains(commands, c => c.counter < 3);
    }

    [Fact]
    public void HardStop_ExecutesOnlyInServerThread()
    {
        var server = new ServerThread();
        var hardStop = new HardStop(server);
        
        Assert.Throws<InvalidOperationException>(hardStop.Execute);
    }
}