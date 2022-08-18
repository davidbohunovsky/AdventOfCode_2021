using System.Drawing;
using System.Numerics;

namespace Day11.TestFiles;

public class DumboOctopus
{
    private int _energy;
    private bool _flashed;
    private Vector2 _position;
    public bool Flashed => _flashed;
    public bool SupposeToFlash => !_flashed && _energy > 9;

    private List<DumboOctopus> _neighborhood = new();
    
    public DumboOctopus(int x, int y, int startEnergy)
    {
        _energy = startEnergy;
        _position = new Vector2(x, y);
    }

    public Vector2[] neighborhoodOffset =
    {
        new (-1, -1),
        new (-1, 0),
        new (-1, 1),
        new (0, -1),
        new (0, 1),
        new (1, -1),
        new (1, 0),
        new (1, 1),
    };
    
    public void InitNeighborhood(DumboOctopus[][] swarm)
    {
        var rows = swarm.Count();
        var columns = swarm[0].Count();
        
        foreach (var neighbor in neighborhoodOffset)
        {
            var neighX = (int)neighbor.X + (int)_position.X;
            var neighY = (int)neighbor.Y + (int)_position.Y;
            
            if(neighX < 0 || neighX >= rows) continue;
            if(neighY < 0 || neighY >= columns) continue;
            
            _neighborhood.Add(swarm[neighX][neighY]);

            // Check boundaries
            // Add if in boundaries
        }
    }
    
    public void AddEnergy()
    {
        _energy += 1;
    }

    public void Flash()
    {
        if (_flashed) return;
        if (!SupposeToFlash) return;

        _flashed = true;
        _neighborhood.ForEach(x =>x.AddEnergy());
    }

    public void Reset()
    {
        if (_flashed)
            _energy = 0;
        
        _flashed = false;
    }
    
}