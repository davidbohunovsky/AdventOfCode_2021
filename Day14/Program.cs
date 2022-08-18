using System.Diagnostics;
using System.Text.RegularExpressions;

#region Extensions

void AddToPolymerDictionary(ref Dictionary<string, long> polymers, string newPolymer, long count)
{
    if (polymers.ContainsKey(newPolymer))
    {
        polymers[newPolymer] += count;
        return;
    }
    
    polymers.Add(newPolymer,count);
}

void AddToCharactersDictionary(ref Dictionary<char, long> characters, char newChar, long count)
{
    if (characters.ContainsKey(newChar))
    {
        characters[newChar] += count;
        return;
    }
    
    characters.Add(newChar,count);
}

#endregion

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var lines = File.ReadAllLines(testDataPath);
var polymerLine = lines.Take(1).Select(x => x).First();

Dictionary<string, char> _polymerRules = new Dictionary<string, char>();

foreach (var line in lines.Skip(2))
{
    var polymerRule = Regex.Replace(line, @"\s+", "").Split("->");
    _polymerRules.Add(polymerRule[0],polymerRule[1].ToCharArray()[0]);
}

Dictionary<string, long> _polymers = new Dictionary<string, long>();
Dictionary<char, long> _characters = new Dictionary<char, long>();

for (int index = 0; index < polymerLine.Length-1; index++)
{
    var rule = polymerLine.Substring(index, 2);
    
    AddToPolymerDictionary(ref _polymers,rule,1);
    AddToCharactersDictionary(ref _characters,rule[0],1);
    AddToCharactersDictionary(ref _characters,rule[1],1);
}

var insertionSteps = 40;

for (var i = 0; i < insertionSteps; i++)
{
    Console.WriteLine($"Step {i+1}/{insertionSteps}");
    Dictionary<string, long> _newPolymers = new Dictionary<string, long>();
    
    foreach (var polymer in _polymers)
    {
        var polymerRule = polymer.Key;
        if (!_polymerRules.ContainsKey(polymerRule)) continue;

        var resultChar = _polymerRules[polymerRule];
            
        AddToCharactersDictionary(ref _characters, resultChar, polymer.Value);
        AddToPolymerDictionary(ref _newPolymers,$"{polymerRule[0]}{resultChar}", polymer.Value);
        AddToPolymerDictionary(ref _newPolymers,$"{resultChar}{polymerRule[1]}", polymer.Value);
    }

    _polymers = _newPolymers;
}

var sorted = _characters.OrderBy(x => x.Value);

foreach (var kvp in sorted)
{
    Console.WriteLine($"{kvp.Key} {kvp.Value}");
}

var mostCommon = sorted.Last().Value;
var leastCommon = sorted.First().Value;

Console.WriteLine(mostCommon - leastCommon);
// There is a bug that sometimes the number is one less 

#region OLD SOLUTION
 
/*
for (int i = 0; i < insertionSteps; i++)
{
    Console.WriteLine($"Round: {i}");
    if (polymer == null) return;
    StringBuilder polymerBuilder = new StringBuilder();
    for (int index = 0; index < polymer.Length-1; index++)
    {
        var rule = polymer.Substring(index, 2);
        polymerBuilder.Append(rule[0]);
        
        if(_polymerRules.ContainsKey(rule))
            polymerBuilder.Append(_polymerRules[rule]);
    }

    polymerBuilder.Append(polymer.Last());
    polymer = polymerBuilder.ToString();
}

var groups = polymer.ToCharArray().ToList().GroupBy(x => x).OrderBy(x => x.Count());
groups.ToList().ForEach(x => Console.WriteLine($"{x.Key} > {x.Count()}"));

var mostCommon = groups.Last().Count();
var leastCommon = groups.First().Count();

Console.WriteLine(mostCommon - leastCommon);
*/

#endregion


