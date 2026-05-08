namespace VoidJumper.Systems;

public class SharpDecorator : WeaponDecorator
{
    private readonly int _bonus;

    public SharpDecorator(IWeapon weapon, int bonus) : base(weapon)
    {
        _bonus = bonus;
    }

    public override int GetDamage() => _weapon.GetDamage() + _bonus;
    public override string GetDescription() => _weapon.GetDescription() + " + Sharp";
}
