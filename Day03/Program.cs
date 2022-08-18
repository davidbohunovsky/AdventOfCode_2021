string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

string[] lines = File.ReadAllLines(testDataPath);

string gamma = "";
string epsilonRate = "";

for(int i = 0; i < lines[0].Length; i++)
{
    var byteArray = lines.Select(x => x[i]).ToArray();
    var most = byteArray.GroupBy(x => x).OrderByDescending(grp => grp.Count()).Select(x => x.Key).First();
    var least = byteArray.GroupBy(x => x).OrderBy(grp => grp.Count()).Select(x => x.Key).First();

    gamma += most;
    epsilonRate += least;
}

var gammaValue = Convert.ToInt32(gamma, 2);
var epsilonRateValue = Convert.ToInt32(epsilonRate, 2);
    
Console.WriteLine($"Day 03 Part 1 result {gammaValue*epsilonRateValue}");

var oxygenGeneratorRating = "";
var CO2ScrubberRating = "";
var array = lines;

for(int i = 0; i < lines[0].Length; i++) 
{
    var byteArray = array.Select(x => x[i]).ToArray();
    var most =  byteArray.GroupBy(x => x).Select(g =>g.Count()).Distinct().Count() == 1
        ? '1'
        : byteArray.GroupBy(x => x).OrderByDescending(grp => grp.Count()).Select(x => x.Key).First();

    array = array.Where(x => x[i] == most).ToArray();
    if (array.Length == 1)
    {
        oxygenGeneratorRating = array[0];
        break;
    }
}

array = lines;
for(int i = 0; i < lines[0].Length; i++) 
{
    var byteArray = array.Select(x => x[i]).ToArray();
    var most =  byteArray.GroupBy(x => x).Select(g =>g.Count()).Distinct().Count() == 1
        ? '0'
        : byteArray.GroupBy(x => x).OrderBy(grp => grp.Count()).Select(x => x.Key).First();

    array = array.Where(x => x[i] == most).ToArray();
    if (array.Length == 1)
    {
        CO2ScrubberRating = array[0];
        break;
    }
}

Console.WriteLine(oxygenGeneratorRating);
Console.WriteLine(CO2ScrubberRating);

var oxygenGeneratorRatingValue = Convert.ToInt32(oxygenGeneratorRating, 2);
var CO2ScrubberRatingValue = Convert.ToInt32(CO2ScrubberRating, 2);

Console.WriteLine($"Day 03 Part 2 result {oxygenGeneratorRatingValue * CO2ScrubberRatingValue}");