
int GetErrorValue(char error, bool autocomplete)
{
    return error switch
    {
        ')' => autocomplete ? 1 : 3,
        ']' => autocomplete ? 2 : 57,
        '}' => autocomplete ? 3 : 1197,
        '>' => autocomplete ? 4 : 25137,
        _ => 0
    };
}

char[] openings = { '(', '[', '{', '<' };
char[] closings = { ')', ']', '}', '>' };

bool CheckCorrectClosing(char lastOpening, char possibleClosing)
{
    int openingIndex = Array.IndexOf(openings, lastOpening);
    int closingIndex = Array.IndexOf(closings, possibleClosing);
    return openingIndex == closingIndex;
}

char GetClosingChar(char lastOpening)
{
    int openingIndex = Array.IndexOf(openings, lastOpening);
    return closings[openingIndex];
}

long CalculateClosingValue(string? lineClosing)
{
    long closingValue = 0;
    if (lineClosing == null) return closingValue;
    
    foreach (var closingChar in lineClosing)
    {
        closingValue *= 5;
        closingValue += GetErrorValue(closingChar, true);
    }

    return closingValue;
}

List<char> illegalSymbols = new List<char>();

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";
var navigations = File.ReadAllLines(testDataPath);

Stack<char> openingSymbols = new Stack<char>();
List<string?> incompleteLines = new List<string?>();

foreach (var navigationLine in navigations)
{
    openingSymbols.Clear();
    var corrupted = false;
    
    foreach (var symbol in navigationLine)
    {
        if (openings.Contains(symbol))
        {
            openingSymbols.Push(symbol);
            continue;
        }
        
        // if empty = illegal
        if (openingSymbols.Count == 0)
        {
            illegalSymbols.Add(symbol);
            corrupted = true;
            break;
        }
        
        // if not empty check if legal 
        if (!CheckCorrectClosing(openingSymbols.Pop(), symbol))
        {
            illegalSymbols.Add(symbol);
            corrupted = true;
            break;
        }
    }
    
    if (!corrupted)
    {
        var openingString = new string(openingSymbols.ToArray());
        incompleteLines.Add(openingString);
    }
}

Console.WriteLine($"Part 1: {illegalSymbols.Select(x => GetErrorValue(x,false)).Sum()}");

List<string?> incompleteLineClosings = new List<string?>();

foreach (var incompleteLine in incompleteLines)
{
    var chars = incompleteLine.ToCharArray().ToList();
    var lineClosing = chars.Select(GetClosingChar).ToArray();
    incompleteLineClosings.Add(new string(lineClosing));
}

incompleteLineClosings.ForEach(Console.WriteLine);

var sortingResults = incompleteLineClosings.Select(CalculateClosingValue).OrderBy(x => x).ToList();

sortingResults.ForEach(Console.WriteLine);

Console.WriteLine($"Part 2: {sortingResults.ElementAt(sortingResults.Count() / 2)}");
