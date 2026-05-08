namespace VoidJumper.Systems;

public class FireDecorator : WeaponDecorator
{
    private readonly int _bonus;

    public FireDecorator(IWeapon weapon, int bonus) : base(weapon)
    {
        _bonus = bonus;
    }

    public override int GetDamage() => _weapon.GetDamage() + _bonus;
    public override string GetDescription() => _weapon.GetDescription() + " + Fire";
}
