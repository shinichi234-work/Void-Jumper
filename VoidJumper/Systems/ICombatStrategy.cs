using VoidJumper.Entities;

namespace VoidJumper.Systems;

public interface ICombatStrategy
{
    void Execute(Enemy enemy);
}
