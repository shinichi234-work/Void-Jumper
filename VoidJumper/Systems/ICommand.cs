namespace VoidJumper.Systems;

public interface ICommand
{
    void Execute();
    void Undo();
}
