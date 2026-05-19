using VoidJumper.Core;

namespace VoidJumper.States;

public abstract class GameState
{
    public virtual void Enter(Game context) { }
    public abstract void HandleInput(Game context, ConsoleKey key);
    public abstract void Update(Game context);
    public abstract void Draw(Game context);
}
