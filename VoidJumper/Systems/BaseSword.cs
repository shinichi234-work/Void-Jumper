namespace VoidJumper.Systems;

public class BaseSword : IWeapon
{
    private const int BaseDamage = 10;

    public int GetDamage() => BaseDamage;
    public string GetDescription() => "Basic Sword";
}
