string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

string line = File.ReadAllText(testDataPath);

var values = line.Split(",").Select(int.Parse);

// Median 
var sortedValues = values.OrderBy(x => x).ToList();
var midPoint = (sortedValues.Count - 1) / 2;
var median = (sortedValues[midPoint] + sortedValues[(int)(midPoint + 0.5f)]) / 2;

var fuelValues = values.Select(x => Math.Abs(x - median));

Console.WriteLine($"Solution one {fuelValues.Sum()}");


var fuelCost = 1;

#region TODO

// TODO
// How to get number 5 in median

/*
while (values.Distinct().Count() != 1)
{
    totalFuelCost += values.Count(x => x != median) * fuelCost;
    var newValues = values.Select(x =>
    {
        if (x < median) return x + 1;
        if (x > median) return x - 1;
        return x;
    });

    values = newValues;
    fuelCost++;
}
*/

#endregion

//  nth triangle number solution
var totalFuelCost = 0;
int? bestFuelCost = null;
var bestChoice = 0;

foreach (var potentionalChoice in Enumerable.Range(values.Min(), values.Max()))
{
    totalFuelCost = values.Select(x =>
    {
        var count = Math.Abs(x - potentionalChoice);
        return count * (count + 1) / 2;
    }).Sum();
    
    
    if (bestFuelCost == null || totalFuelCost < bestFuelCost)
    {
        bestFuelCost = totalFuelCost;
        bestChoice = potentionalChoice;
    }
}

Console.WriteLine($"Solution 2 cost {bestFuelCost} fuel with number {bestChoice}");
