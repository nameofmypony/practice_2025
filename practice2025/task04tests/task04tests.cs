namespace task04tests;

using task04;
using Xunit;

public class SpaceshipTests
{
    [Fact]
    public void Cruiser_ShouldHaveCorrectStats()
    {
        var cruiser = new Cruiser();
        Assert.Equal(50, cruiser.Speed);
        Assert.Equal(10, cruiser.FirePower);
        Assert.Equal(0, cruiser.Position);
        Assert.Equal(0, cruiser.Angle);
        Assert.Equal(50, cruiser.Bullets);
    }

    [Fact]
    public void Fighter_ShouldHaveCorrectStats()
    {
        var fighter = new Fighter();
        Assert.Equal(100, fighter.Speed);
        Assert.Equal(5, fighter.FirePower);
        Assert.Equal(0, fighter.Position);
        Assert.Equal(0, fighter.Angle);
        Assert.Equal(100, fighter.Bullets);
    }

    [Fact]
    public void Fighter_ShouldBeFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Speed > cruiser.Speed);
    }

    [Fact]
    public void Fighter_ShouldMoveFasterThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        fighter.MoveForward();
        cruiser.MoveForward();
        Assert.True(fighter.Position > cruiser.Position);
    }

    [Fact]
    public void Fighter_ShouldRotateCorrectly()
    {
        var fighter = new Fighter();
        fighter.Rotate(90);
        Assert.Equal(90, fighter.Angle);
    }

    [Fact]
    public void Cruiser_ShouldRotateCorrectly()
    {
        var cruiser = new Cruiser();
        cruiser.Rotate(30);
        Assert.Equal(30, cruiser.Angle);
    }

    [Fact]
    public void Cruiser_ShouldHaveMoreFirePowerThanFighter()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.FirePower < cruiser.FirePower);
    }
    
    [Fact]
    public void Fighter_ShouldHaveMoreBulletsThanCruiser()
    {
        var fighter = new Fighter();
        var cruiser = new Cruiser();
        Assert.True(fighter.Bullets > cruiser.Bullets);
    }
}