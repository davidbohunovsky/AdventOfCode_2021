using Day11.TestFiles;

string dumbDataPath = @"TestFiles\dumbData.txt";

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var text = File.ReadAllLines(testDataPath);
var rows = text.Length;
var columns = 0;

int[][] energyValues = new int[rows][];
var row = 0;

foreach (var line in text)
{
    var values = line.Select(x => int.Parse(x.ToString())).ToArray();
    columns = values.Length;
    energyValues[row] = new int[columns];
    energyValues[row] = values;
    row += 1;
}

DumboOctopus[][] octoSwarm = new DumboOctopus[rows][];

for (var x = 0; x < rows; x++)
{
    octoSwarm[x] = new DumboOctopus[columns];
    for (var y = 0; y < columns; y++)
    {
        octoSwarm[x][y] = new DumboOctopus(x, y, energyValues[x][y]);
    }
}

var swarmList = octoSwarm.SelectMany(x => x).ToList();
swarmList.ForEach(x => x.InitNeighborhood(octoSwarm));

/*
for (var x = 0; x < rows; x++)
{
    string lineText = "";
    for (var y = 0; y < columns; y++)
    {
        lineText += octoSwarm[x][y].NumberOfNeighs;
    }
    Console.WriteLine(lineText);
}
*/
    

var steps = 100;
var flashes = 0;

int step = 0;
//for (int i = 0; i < steps; i++)
while(true)
{
    swarmList.ForEach(x => x.AddEnergy());

    /*
    if (swarmList.TrueForAll(x => x.SupposedToFlash))
    {
        Console.WriteLine($"Part 2: {step}");
        break;
    }
    */
    
    while (swarmList.Count(x => x.SupposeToFlash) > 0)
    {
        swarmList.ForEach(x => x.Flash());
    }
    
    flashes += swarmList.Count(x => x.Flashed);
    step++;
    
    if (step == steps)
    {
        Console.WriteLine($"Part 1: {flashes}");
    }
    
    if (swarmList.TrueForAll(x => x.Flashed))
    {
        Console.WriteLine($"Part 2: {step}");
        break;
    }
    
    swarmList.ForEach(x => x.Reset());
}

