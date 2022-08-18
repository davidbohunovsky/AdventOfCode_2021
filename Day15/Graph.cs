using System.Numerics;
using System.Text;

namespace Day15;

public class Graph
{
    private class RiskNode
    {
        public string Name { get; private set; }
        public int RiskValue { get; private set; }
        public Vector2Int Cords { get; private set; }
        public float RiskPath { get; set; }
        public RiskNode? Previous { get; set; }
        
        public RiskNode(string name, int value, Vector2Int cords)
        {
            Name = name;
            RiskValue = value;
            Cords = cords;

            RiskPath = float.PositiveInfinity;
            Previous = null;
        }
    }
    
    private int _width;
    private int _height;

    private RiskNode[,] _graph;
    
    private List<RiskNode> _unvisitedNodes;
    private List<RiskNode> _visitedNodes;
    
    public Vector2Int[] _edges =
    {
        new (-1, 0),
        new (1, 0),
        new (0, -1),
        new (0, 1),
    };
    
    public Graph(int x, int y)
    {
        _width = x;
        _height = y;
        
        _graph = new RiskNode[_width, _height];
        
        _unvisitedNodes = new List<RiskNode>();
        _visitedNodes = new List<RiskNode>();
    }

    public void Show()
    {
        var sb = new StringBuilder();
        Console.Write(_graph);
        for (var x = 0; x < _width; x++)
        { 
            for (var y = 0; y < _height; y++)
            {
                sb.Append(_graph[x, y].RiskValue);
                sb.Append(' ');
            }
            sb.AppendLine();
        }
    
        Console.WriteLine(sb.ToString());
    }
    
    public void AddNode(int x, int y, int riskValue)
    {
        var node =  new RiskNode($"{x}{y}",riskValue, new Vector2Int(x,y));

        _graph[x, y] = node;
        _unvisitedNodes.Add(node);
    }
    
    public int FindShortestPath(Vector2Int from, Vector2Int to)
    {
         // Works but too slow :D 
         // Try priority Queue
         
        if (!InBounds(from.X, from.Y)) return -1;
        if (!InBounds(to.X, to.Y)) return -1;

        if(_visitedNodes.Count > 0) 
            Reset();

        var startingNode = _graph[from.X, from.Y];
        startingNode.RiskPath = 0;
        
        while (_unvisitedNodes.Count > 0)
        {
            Console.WriteLine($"Node: {_visitedNodes.Count} / {_graph.Length}");
            
            var currentNode = _unvisitedNodes
                .Where(x => !float.IsPositiveInfinity(x.RiskPath))
                .OrderBy(x => x.RiskPath)
                .First();
            
            var pathNodes = UnvisitedNodes(currentNode);
            foreach (var pathNode in pathNodes)
            {
                var possiblePathValue = currentNode.RiskPath + pathNode.RiskValue;
                pathNode.RiskPath = possiblePathValue < pathNode.RiskPath
                    ? possiblePathValue
                    : pathNode.RiskPath;
            }
            
            _visitedNodes.Add(currentNode);
            _unvisitedNodes.Remove(currentNode);
        }
        
        return (int)_graph[to.X, to.Y].RiskPath;

        List<RiskNode> UnvisitedNodes(RiskNode start)
        {
            var unvisitedNodes = new List<RiskNode>();
            
            foreach (var edgePosition in _edges)
            {
                var x = edgePosition.X + start.Cords.X;
                var y = edgePosition.Y + start.Cords.Y;
            
                if(x < 0 || x >= _width) continue;
                if(y < 0 || y >= _height) continue;

                if (_visitedNodes.Contains(_graph[x, y])) continue;
                unvisitedNodes.Add(_graph[x,y]);
            }
            
            return unvisitedNodes;
        }
    }


    private void Reset()
    {
        _unvisitedNodes.ForEach(ResetFunc);
        _visitedNodes.Clear();

        void ResetFunc(RiskNode node)
        {
            node.RiskPath = float.PositiveInfinity;
            node.Previous = null;
        }
    }
    
    private bool InBounds(int x, int y)
    {
        if (x < 0 || x >= _width ) return false;
        if (y < 0 || y >= _height) return false;

        return true;
    }
}