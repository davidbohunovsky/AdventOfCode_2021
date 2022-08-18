namespace Day2;

public static class Extensions
{
    public static Command ToCommand(this string commandString)
    {
        string[] values = commandString.Split(" ");
        Direction direction = default;
        
        try
        {
            // should never be null
            direction = Enum.Parse<Direction>(values[0],true);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
        int value = 0;
        int.TryParse(values[1], out value);

        return new Command
        {
            Direction = direction,
            Value = value
        };
    }
}