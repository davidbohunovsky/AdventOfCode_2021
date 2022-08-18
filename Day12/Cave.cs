namespace Day12;

public class Cave
{
    private string _name;
    private bool _big;

    private List<Cave> _paths;

    public string Name => _name;
    public bool Big => _big;

    public List<Cave> Paths => _paths;

    public Cave(string name, bool big)
    {
        _paths = new List<Cave>();
        _name = name;
        _big = big;
    }

    public void AddPath(Cave path)
    {
        _paths.Add(path);
    }

    // Small duplicate - Task 2 - One small room can be visited twice
    public bool Visitable(List<string> visitedCaves, bool smallDuplicate)
    {
        if (!smallDuplicate)
        {
            return Big || visitedCaves.All(x => x != Name);
        }

        var smallCaves = visitedCaves.Where(x => Char.IsLower(x[0]));
        bool alreadyDuplicate = smallCaves.GroupBy(x => x).Count(x => x.Count() > 1) > 0;

        //Console.WriteLine(alreadyDuplicate);
        
        if (alreadyDuplicate)
            return Big || visitedCaves.All(x => x != Name);

        return true; // return big = big dumbo :) :) :)
    }
}