using System.Text;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

List<int> xValues = new();
List<int> yValues = new();
List<Command> commands = new();
bool readCommands = false;

// Read file
var lines = File.ReadAllLines(testDataPath);
foreach(var line in lines)
{
    if (line == String.Empty)
    {
        readCommands = true;
        continue;
    }

    if (readCommands)
    {
        var command = line.Split(" ")[2];
        var cParams = command.Split("=");
        
        commands.Add( new Command
        {
            FoldAxis = cParams[0] == "x" ? Axis.X : Axis.Y,
            Fold = int.Parse(cParams[1])
        });
        continue;
    }

    var values = line.Split(",");
    xValues.Add(int.Parse(values[0]));
    yValues.Add(int.Parse(values[1]));
}

int xSize = xValues.Max()+ 1;
int ySize = yValues.Max()+ 1;

// Create array
char[,] foldArray = new char[xSize, ySize];

for (var x = 0; x < xSize; x++)
{
    for (var y = 0;y < ySize; y++)
    {
        foldArray[x, y] = '.';
    }
}

foreach (var cords in xValues.Zip(yValues, (x,y) => new { xValue = x, yValue = y }))
{
    //Console.WriteLine($"X:{cords.xValue} Y:{cords.yValue}");
    foldArray[cords.xValue,cords.yValue] = '#';
}

var numberOfFolds = commands.Count;

foldArray = commands.Take(numberOfFolds).Aggregate(foldArray, Fold);

DrawArray(foldArray);
Console.WriteLine(Flatten(foldArray).Count(x => x == '#'));

void DrawArray(char[,] array)
{
    StringBuilder sb = new StringBuilder();
    for (var y = 0; y < ySize; y++)
    { 
        for (var x = 0; x < xSize; x++)
        {
            sb.Append(array[x, y]);
            sb.Append(' ');
        }
        sb.AppendLine();
    }
    
    Console.WriteLine(sb.ToString());
}

void DrawFoldLine(char[,] array, Command foldCommand)
{
    switch (foldCommand.FoldAxis)
    {
        case Axis.X:
            for (var y = 0; y < ySize; y++)
            {
                array[foldCommand.Fold, y] = '|';
            }
            break;
        
        case Axis.Y:
            for (var x = 0; x < xSize; x++)
            {
                array[x, foldCommand.Fold] = '_';
            }
            break;
    }
    
    DrawArray(array);
}

char[,] Fold(char[,] array, Command foldCommand)
{
    xSize = array.GetLength(0);
    ySize = array.GetLength(1);
    
    var foldedArray = new char[0, 0];
    
    switch (foldCommand.FoldAxis)
    {
        case Axis.X:
            foldedArray = new char[foldCommand.Fold, ySize];
            
            for (var x = 0; x < foldCommand.Fold; x++)
            {
                for (var y = 0;y < ySize; y++)
                {
                    foldedArray[x, y] = GetFoldValue(x, y, (xSize-1) - x, y);
                }
            }

            xSize = foldCommand.Fold;
            break;
        
        case Axis.Y:
            foldedArray = new char[xSize,foldCommand.Fold];
            
            for (var x = 0; x < xSize; x++)
            {
                for (var y = 0;y < foldCommand.Fold; y++)
                {
                    foldedArray[x, y] = GetFoldValue(x, y, x, (ySize-1) - y);
                }
            }
            
            ySize = foldCommand.Fold;
            break;
    }
    
    return foldedArray;

    char GetFoldValue(int x, int y, int xOffset, int yOffset)
    {
        //Console.WriteLine($"First:{x},{y} - Second:{xOffset},{yOffset}");
        return array[x, y] == '#' || array[xOffset, yOffset] == '#' ? '#' : '.';
    }
}

List<char> Flatten(char[,] array)
{
    char[] tmp = new char[array.GetLength(0) * array.GetLength(1)];    
    Buffer.BlockCopy(array, 0, tmp, 0, tmp.Length * sizeof(char));
    List<char> list = new List<char>(tmp);
    return list;
}

enum Axis
{
    X,
    Y
}

struct Command
{
    public Axis FoldAxis;
    public int Fold;
}

