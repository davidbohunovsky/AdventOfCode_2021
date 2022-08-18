using System.Numerics;
using Day05;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

string[] lines = File.ReadAllLines(trainDataPath);

//var values = lines.Select(x => x.Split("->")).SelectMany(x => x); return;

var values = lines.Select(x => x.Split("->"));
Dictionary<string,int> diagram = new Dictionary<string, int>();

foreach (var value in values)
{
    var start = value[0].Split(",").Select(int.Parse).ToArray();
    var end = value[1].Split(",").Select(int.Parse).ToArray();

    Vector2 startVector = new Vector2(start[0], start[1]);
    Vector2 endVector = new Vector2(end[0], end[1]);
    
    Console.WriteLine($"{startVector.X}{startVector.Y} -> {endVector.X}{endVector.Y}");
    
    Vector2 direction = new Vector2(endVector.X - startVector.X, endVector.Y - startVector.Y);
    Vector2 normalizedDirection = direction.NormalizedClamp();

    Console.WriteLine(normalizedDirection);
    
    while (Vector2.Distance(startVector, endVector) != 0)
    {
        string key = $"{startVector.X},{startVector.Y}";
        Console.WriteLine(key);
        if (diagram.ContainsKey(key))
        {
            diagram[key]++;
        }
        else
        {
            diagram.Add(key, 1);
        }

        startVector = Vector2.Add(startVector, normalizedDirection);
    }
    
    // Todo
    // Refactor 
    string endKey = $"{endVector.X},{endVector.Y}";
    if (diagram.ContainsKey(endKey))
    {
        diagram[endKey]++;
    }
    else
    {
        diagram.Add(endKey, 1);
    }

}

Console.WriteLine(diagram.Count(x => x.Value > 1));
