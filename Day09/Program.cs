using System.Threading.Channels;
using Day09;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var text = File.ReadAllLines(testDataPath);
var rows = text.Length;
var columns = 0;
            
int[][] intMap = new int[rows][];
var row = 0;

foreach (var line in text)
{
    var values = line.Select(x => int.Parse(x.ToString())).ToArray();
    columns = values.Length;
    intMap[row] = new int[columns];
    intMap[row] = values;
    row += 1;
}

HeatmapPoint[][] heatMap = new HeatmapPoint[rows][];

for (int x = 0; x < rows; x++)
{
    heatMap[x] = new HeatmapPoint[columns];
    for (int y = 0; y < columns; y++)
    {
        heatMap[x][y] = new HeatmapPoint(x, y, intMap[x][y]);
    }
}

var heatArray = heatMap.SelectMany(x => x).ToList();
heatArray.ForEach(x => x.InitNeighbours(heatMap));

var heatPoints = heatArray.Where(x =>x.IsHeatPoint()).ToList();
Console.WriteLine($"Part 1: {heatPoints.Sum(x => x.Value + 1)}");

var basins = new List<HeatmapBasin>();
foreach (var heatPoint in heatPoints)
{
    basins.Add(new HeatmapBasin(heatPoint));
}

var value = basins.Select(x => x.Value).OrderByDescending(x =>x).Take(3).ToList();

Console.WriteLine($"Part 2: {value.Aggregate( (sum ,next) => sum * next)}");



