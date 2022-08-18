using Day12;

string trainDataPath = @"TestFiles\trainData.txt";
string testDataPath = @"TestFiles\testData.txt";

var lines = File.ReadAllLines(testDataPath);

List<Cave> _caves = new List<Cave>();

foreach (var line in lines)
{
    var rooms = line.Split("-");
    foreach (var room in rooms)
    {
        if (!_caves.Exists(x => x.Name == room))
        {
            _caves.Add(new Cave(room,Char.IsUpper(room[0])));
        }
    }

    var pathRooms = _caves.Where(x => rooms.Contains(x.Name)).ToList();
    ConnectPath(pathRooms[0],pathRooms[1]);
}

//_caves.ForEach(x => Console.WriteLine(x.Name));
var start = _caves.First(x => x.Name == "start");
var end = _caves.First(x => x.Name == "end");


var paths = new List<List<string>>();
var finishedPaths = new List<List<string>>();

paths.Add(new List<string>{start.Name });
while (paths.Count != 0)
{
    var newCreatedPaths = new List<List<string>>();
    foreach (var currentPath in paths)
    {
        var first = _caves.First(x => x.Name == currentPath.Last());
        foreach (var possiblePath in first.Paths.Where(x => x.Visitable(currentPath,true)))
        {
            if (possiblePath == start) continue;
            
            if (possiblePath == end)
            {
                var endPath = new List<string>();
                endPath.AddRange(currentPath);
                endPath.Add(possiblePath.Name);
                finishedPaths.Add(endPath);
                continue;
            }

            var newPath = new List<string>();
            newPath.AddRange(currentPath);
            newPath.Add(possiblePath.Name);
            newCreatedPaths.Add(newPath);
        }
    }

    paths = newCreatedPaths.ToList();
    newCreatedPaths.Clear();
}

Console.WriteLine(finishedPaths.Count);

void ConnectPath(Cave a, Cave b)
{
    a.AddPath(b);
    b.AddPath(a);
}