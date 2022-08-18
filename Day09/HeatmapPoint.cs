using System.Numerics;

namespace Day09;

public struct HeatVector2
{
    public HeatVector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X;
    public int Y;
}

public class HeatmapBasin
{
    private HeatmapPoint _startPoint;

    public List<HeatmapPoint> _visitedPoints;
    public Queue<HeatmapPoint> _pointsToVisit;
    
    public int Value => _visitedPoints.Count;
    
    public HeatmapBasin(HeatmapPoint start)
    {
        _startPoint = start;

        _visitedPoints = new List<HeatmapPoint>();
        _pointsToVisit = new Queue<HeatmapPoint>();
        
        CalculatedBasin();
    }

    private void CalculatedBasin()
    {
        _pointsToVisit.Enqueue(_startPoint);
        
        while (_pointsToVisit.Count > 0)
        {
            var currentPoint = _pointsToVisit.Dequeue();
            _visitedPoints.Add(currentPoint);
            var possiblePoints = currentPoint.GetClosePoints();
            
            foreach (var possiblePoint in possiblePoints)
            {
                if(!_visitedPoints.Contains(possiblePoint) && !_pointsToVisit.Contains(possiblePoint))
                    _pointsToVisit.Enqueue(possiblePoint);
            }
        }
    }
}

public class HeatmapPoint
{
    private int _value;
    private HeatVector2 _location;
    
    private HeatmapPoint? _north;
    private HeatmapPoint? _south;
    private HeatmapPoint? _west;
    private HeatmapPoint? _east;
    
    public int Value => _value;

    public HeatmapPoint(int x, int y, int value)
    {
        _value = value;
        _location = new HeatVector2(x, y);
    }

    public void InitNeighbours(HeatmapPoint[][] heatMap)
    {
        var rows = heatMap.Count();
        var columns = heatMap[0].Count();

        // North 
        _north = _location.X - 1 >= 0
            ? heatMap[_location.X - 1][_location.Y]
            : null;

        // South
        _south = _location.X + 1 < rows
            ? heatMap[_location.X + 1][_location.Y]
            : null;

        // West
        _west = _location.Y - 1 >= 0
            ? heatMap[_location.X][_location.Y - 1]
            : null;

        // East
        _east = _location.Y + 1 < columns
            ? heatMap[_location.X][_location.Y + 1]
            : null;
    }

    public bool IsHeatPoint()
    {
        if (_north != null && _north._value <= _value)
            return false;
        
        if (_south != null && _south._value <= _value)
            return false;
        
        if (_west != null && _west._value <= _value)
            return false;
        
        if (_east != null && _east._value <= _value)
            return false;

        return true;
    }

    public List<HeatmapPoint> GetClosePoints()
    {
        List<HeatmapPoint> closePoints = new List<HeatmapPoint>();
        
        // Return points with difference of one;
        if (_north != null && _north.Value != 9)
            closePoints.Add(_north);
        
        if (_south != null && _south.Value != 9)
            closePoints.Add(_south);
        
        if (_west != null && _west.Value != 9)
            closePoints.Add(_west);
        
        if (_east != null &&  _east.Value != 9)
            closePoints.Add(_east);

        return closePoints;
    }
}