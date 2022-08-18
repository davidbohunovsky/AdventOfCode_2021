string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var lines = File.ReadAllLines(testDataPath);

Dictionary<string, int> _signalPatters = new Dictionary<string, int>()
{
    { "acedgfb", 8 },
    { "cdfbe", 5 },
    { "gcdfa", 2 },
    { "fbcad", 3 },
    { "dab", 7 },
    { "cefabd", 9 },
    { "cdfgeb", 6 },
    { "eafb", 4 },
    { "cagedb", 0 },
    { "ab", 1 },
};

bool OneFourSevenEightOutput(string word)
{
    return word.Length is 2 or 3 or 4 or 7;
}

var oneFourSevenEight = 0;

foreach (var line in lines)
{
    var output = line.Split('|')[1];
    oneFourSevenEight += output.Split(' ').ToList().Where(OneFourSevenEightOutput).Count();
}

Console.WriteLine($"Part 1: {oneFourSevenEight}");