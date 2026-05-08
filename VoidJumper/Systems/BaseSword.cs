namespace VoidJumper.Systems;

public class BaseSword : IWeapon
{
    public int GetDamage() => 10;
    public string GetDescription() => "Basic Sword";
}
