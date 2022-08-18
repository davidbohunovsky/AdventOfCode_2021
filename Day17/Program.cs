using System.Diagnostics;
using System.Text.RegularExpressions;
using Day15; // For Vector2Int Reference - Make Separate UTILS project


string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var input = File.ReadAllText(trainDataPath);

Console.WriteLine(input);

/*
// Vyřešit potom
var matches = Regex.Matches(input, @"-?\d+");
foreach (var number in matches)
{
    Console.WriteLine("Number: {0}", number);
}
*/

var testValues = new int[] {119,176,-141,-84};
var trainValues = new int[] {20,30,-10,-5};

// Precondition - Always 4 Values
var rectInput = testValues;
if (rectInput.Length != 4) return;

var boundaryRect = new Rect(rectInput);
Console.WriteLine(boundaryRect);
var initialPosition = new Vector2Int(0, 0);

var startingVelocityValues = new Vector2Int(0, -200);
var startingVelocityOffsets = new Vector2Int(200, 200); 

var results = new List<Result>();

for (var offsetX = startingVelocityValues.X; offsetX < startingVelocityOffsets.X; offsetX++)
{
    for (var offsetY = startingVelocityValues.Y; offsetY < startingVelocityOffsets.Y; offsetY++)
    {
        var velocity = new Vector2Int(offsetX, offsetY);
        var result = Shoot(initialPosition, velocity, boundaryRect);

        if (result.Hit)
        {
            results.Add(result);
        }
        
    }
}

var maximum = results
    .Where(x => x.Hit)
    .Select(x => x.MaximumHeight)
    .Max();

Console.WriteLine($"Maximum height {maximum}");
Console.WriteLine($"Possible hits {results.Select(x => x.InitialVelocity).Distinct().Count()}");

Result Shoot(Vector2Int startPosition, Vector2Int startVelocity, Rect destination)
{
    var maximumHeight = 0;

    var currentPosition = startPosition;
    var currentVelocity = startVelocity;

    while (!destination.Missed(currentPosition))
    {
        currentPosition.X += currentVelocity.X;
        currentPosition.Y += currentVelocity.Y;
        maximumHeight = maximumHeight < currentPosition.Y
            ? currentPosition.Y
            : maximumHeight;
        
        if (destination.InBoundary(currentPosition))
        {
            //Console.WriteLine($"{startVelocity} - HIT");
            return new Result
            {
                InitialVelocity = startVelocity,
                Hit = true,
                MaximumHeight = maximumHeight
            };

        }
        ApplyForces(ref currentVelocity);
    }

    //Console.WriteLine($"{startVelocity} - MISS");
    return new Result
    {
        Hit = false,
        MaximumHeight = maximumHeight
    };
    
    void ApplyForces(ref Vector2Int velocity)
    {
        velocity.X = ApplyDrag(velocity.X);
        velocity.Y = ApplyGravity(velocity.Y);
        
        int ApplyDrag(int x)
        {
            if (x == 0) return x;
            return x < 0 ? x+1 : x-1;
        }

        int ApplyGravity(int y)
        {
            return y - 1;
        }
    }
}

struct Result
{
    public Vector2Int InitialVelocity;
    public bool Hit;
    public int MaximumHeight;
}

internal class Rect
{
    private readonly int _x1;
    private readonly int _x2;
    private readonly int _y1;
    private readonly int _y2;
    
    public Rect(int[] cords)
    {
        if (cords[0] > cords[1])
        {
            _x1 = cords[1];
            _x2 = cords[0];
        }
        else
        {
            _x1 = cords[0];
            _x2 = cords[1];
        }
        
        if (cords[2] > cords[3])
        {
            _y1 = cords[2];
            _y2 = cords[3];
        }
        else
        {
            _y1 = cords[3];
            _y2 = cords[2];
        }
    }
    
    public bool InBoundary(Vector2Int position)
    {
        if (position.X <_x1) return false;
        if (position.X > _x2) return false;
        if (position.Y > _y1) return false;
        if (position.Y < _y2) return false;
        
        return true;
    }

    public bool Missed(Vector2Int position)
    {
        return position.X > _x2 || position.Y < _y2;
    }
    
    public override string ToString()
    {
        return $"Rect with X1:{_x1} X2:{_x2} Y1:{_y1} Y2:{_y2}";
    }
}

