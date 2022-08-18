using Day1;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var resultCount = SolutionA.Solution(testDataPath);
Console.WriteLine($"Day 01 Part 1 result: {resultCount}");

resultCount = SolutionB.Solution(testDataPath);
Console.WriteLine($"Day 01 Part 2 result: {resultCount}");
    




