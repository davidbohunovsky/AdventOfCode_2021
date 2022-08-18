using System.Net.Cache;
using Day15;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var lines = File.ReadAllLines(testDataPath);

int width = lines[0].Length;
int height = lines.Length;

int caveDimension = 5;

Console.WriteLine($"W:{width * caveDimension} H:{height* caveDimension}");

var riskGraph = new Graph(width * caveDimension, height * caveDimension);

int x = 0;
int y = 0;

List<Vector2Int> _caveOffsets = new List<Vector2Int>();

for (var offsetX = 0; offsetX < caveDimension; offsetX++)
{
    for (var offsetY = 0; offsetY < caveDimension; offsetY++)
    {
        if (offsetX == 0 && offsetY == 0) continue; // Base Node;
        _caveOffsets.Add(new Vector2Int(offsetX * width, offsetY * height));
    }
}

_caveOffsets.ForEach(x => Console.WriteLine(x));

foreach(var row in lines)
{
    foreach (var currentRisk in row.Select(riskV => riskV - '0'))
    {
        riskGraph.AddNode(x,y,currentRisk);
        foreach (var offset in _caveOffsets)
        {
            var offSetRisk = RiskOffset(currentRisk,offset.X/width + offset.Y/height);
            riskGraph.AddNode(offset.X + x,offset.Y + y, offSetRisk);
        }
        
        y++;
    }
    
    x++;
    y = 0;

    int RiskOffset(int currentRisk, int offset)
    {
        currentRisk += offset;
        return currentRisk > 9 ? currentRisk - 9 : currentRisk;
    }
}

//riskGraph.Show();

Vector2Int from = new Vector2Int(0, 0);
Vector2Int to = new Vector2Int(width * caveDimension - 1, height * caveDimension - 1);

Console.WriteLine($"Lower risk path value: {riskGraph.FindShortestPath(from,to)}");