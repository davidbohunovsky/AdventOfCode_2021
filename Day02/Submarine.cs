namespace Day2;

public enum Direction
{
    Forward,
    Down,
    Up
}

public struct Command
{
    public Direction Direction;
    public int Value;
}

public class Submarine
{
    private int _depth;
    private int _forwardPosition;
    private int _aim;
    
    public int Position => _depth * _forwardPosition;
    
    public Submarine()
    {
        _aim = 0;
        _depth = 0;
        _forwardPosition = 0;
    }

    public void Move(Command command)
    {
        switch (command.Direction)
        {
            case Direction.Up:
                //_depth -= command.Value;
                _aim -= command.Value;
                break;
            
            case Direction.Down:
                //_depth += command.Value;
                _aim += command.Value;
                break;
            
            case Direction.Forward:
                _depth += _aim * command.Value;
                _forwardPosition += command.Value;
                break;
        }
    }
}