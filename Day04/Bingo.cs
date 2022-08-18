using System.Data;
using System.Diagnostics;
using Microsoft.VisualBasic;

namespace Day4;

public struct BingoTile
{
    public int Value;
    public bool Hit;
}

public class Bingo
{
    private BingoTile[][] _bingoBoard;
    private int _boardSizeX;
    private int _boardSizeY;
    private int _boardSize;

    private bool _win;

    public bool Win => _win;
    
    public Bingo(int x, int y)
    {
        _win = false;
        
        _boardSizeX = x;
        _boardSizeY = y;

        _boardSize = _boardSizeX * _boardSizeY;
        
        _bingoBoard = new BingoTile[x][];
        for (int i = 0; i < _boardSizeX; i++)
        {
            _bingoBoard[i] = new BingoTile[y];
        }
    }

    public void Init(string[] lines)
    {
        int[][] bingoValues = new int[_boardSizeX][];
        
        for (int i = 0; i < _boardSizeX; i++)
        {
            var values = lines[i].Split(" ").Where(x=> x != string.Empty).ToArray();
            Console.WriteLine($"{values[0]} {values[1]} {values[2]} {values[3]} {values[4]}");
            bingoValues[i] = Array.ConvertAll(values, int.Parse);
        }
        
        for (int x = 0; x < _boardSizeX; x++)
        {
            for (int y = 0; y < _boardSizeY; y++)
            {
                _bingoBoard[x][y].Value = bingoValues[x][y];
                _bingoBoard[x][y].Hit = false;
            }
        }
    }

    public bool DrawBingo(int draw)
    {
        for (int x = 0; x < _boardSizeX; x++)
        {
            for (int y = 0; y < _boardSizeY; y++)
            {
                if (_bingoBoard[x][y].Value == draw)
                    _bingoBoard[x][y].Hit = true;
            }
        }
        
        _win = CheckRows() || CheckColumns();
        return _win;
    }
    
    private bool CheckRows()
    {
        foreach (var row in _bingoBoard)
        {
            if(row.All(x => x.Hit)){
            {
                return true;
            }}
        }
        
        return false;
    }

    private bool CheckColumns()
    {
        var flatten = _bingoBoard.SelectMany(x => x).ToArray();

        for (int i = 0; i < _boardSizeX; i++)
        {
            var hits = 0;
            for (int j = 0; j < _boardSize; j += 5)
            {
                if (flatten[j + i].Hit)
                    hits++;
            }

            if (hits == 5) return true;
        }

        return false;
    }

    public int UnmarkedSum()
    {
        var flatten = _bingoBoard.SelectMany(x => x).ToArray();
        return flatten.Where(x => x.Hit == false).Select(x => x.Value).Sum();
    }
}