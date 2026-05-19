namespace VoidJumper.Systems;

public class InputManager
{
    private readonly Dictionary<ConsoleKey, ICommand> _bindings = new();
    private readonly Stack<ICommand> _history = new();

    public void Bind(ConsoleKey key, ICommand command)
    {
        _bindings[key] = command;
    }

    public void Handle(ConsoleKey key)
    {
        if (_bindings.TryGetValue(key, out var command))
        {
            command.Execute();
            _history.Push(command);
        }
    }

    public void Undo()
    {
        if (_history.Count > 0)
            _history.Pop().Undo();
    }
}
