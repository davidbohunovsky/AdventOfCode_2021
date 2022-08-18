// See https://aka.ms/new-console-template for more information

using Day4;

string trainInputDataPath = @"TestFiles\trainInputData.txt";
string testInputDataPath = @"TestFiles\testInputData.txt";

string trainDataPath = @"TestFiles\trainBingo.txt";
string testDataPath = @"TestFiles\testBingo.txt";


string[] lines = File.ReadAllLines(testDataPath);
int[] bingoDraws = Array.ConvertAll(File.ReadAllText(testInputDataPath).Split(","),int.Parse);

// Counting that bingo is same width and height 
var count = lines[0].Split(" ").Count(x => x!= string.Empty);

List<Bingo> _bingos = new List<Bingo>();

List<string> bingoLines = new List<string>();
foreach (var line in lines)
{
    if (line == string.Empty)
    {
        var bingo = new Bingo(count, count);
        bingo.Init(bingoLines.ToArray());
        _bingos.Add(bingo);
        bingoLines = new List<string>();
    }
    else
    {
        bingoLines.Add(line);
    }
}

// TODO Refactor
var Lastbingo = new Bingo(count, count);
Lastbingo.Init(bingoLines.ToArray());
_bingos.Add(Lastbingo);

int winBingos = 0;

foreach (var draw in bingoDraws)
{
    foreach (var bingo in _bingos)
    {
        if (!bingo.Win)
        {
            if (bingo.DrawBingo(draw))
            {
                winBingos++;
                if (winBingos == _bingos.Count)
                {
                    Console.WriteLine($"{draw} {bingo.UnmarkedSum()}");
                    Console.WriteLine(draw * bingo.UnmarkedSum());
                    return;
                }
            }
        }
    }
}
