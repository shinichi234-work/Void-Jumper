using VoidJumper.Systems;

namespace VoidJumper.Tests;

public class WeaponDecoratorTests
{
    [Fact]
    public void BaseSword_GetDamage_ShouldReturnTen()
    {
        var sword = new BaseSword();

        var damage = sword.GetDamage();

        Assert.Equal(10, damage);
    }

    [Fact]
    public void FireDecorator_GetDamage_ShouldAddBonus()
    {
        var sword = new FireDecorator(new BaseSword(), 5);

        var damage = sword.GetDamage();

        Assert.Equal(15, damage);
    }

    [Fact]
    public void SharpDecorator_GetDamage_ShouldAddBonus()
    {
        var sword = new SharpDecorator(new BaseSword(), 3);

        var damage = sword.GetDamage();

        Assert.Equal(13, damage);
    }

    [Fact]
    public void ChainedDecorators_GetDamage_ShouldStackBonuses()
    {
        var sword = new SharpDecorator(new FireDecorator(new BaseSword(), 5), 3);

        var damage = sword.GetDamage();

        Assert.Equal(18, damage);
    }

    [Fact]
    public void ChainedDecorators_GetDescription_ShouldIncludeAllParts()
    {
        var sword = new SharpDecorator(new FireDecorator(new BaseSword(), 5), 3);

        var description = sword.GetDescription();

        Assert.Contains("Fire", description);
        Assert.Contains("Sharp", description);
    }
}
