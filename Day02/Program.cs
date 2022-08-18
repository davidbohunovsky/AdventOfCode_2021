using Day2;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var submarine = new Submarine();

string[] lines = File.ReadAllLines(testDataPath);

foreach (var line in lines)
{
    submarine.Move(line.ToCommand());
}

Console.WriteLine(submarine.Position);