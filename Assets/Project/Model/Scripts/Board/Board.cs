using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

internal class Board
{
    Othello[,] _grid;
    List<Vector2Int> _setCandidates = new List<Vector2Int>();
    internal List<Vector2Int> SettablePositions { get; private set; }

    bool _isStarted = false;

    readonly int _sideLength = 8;
    readonly Vector2Int[] _direction =
    {
        new Vector2Int(0, 1), //north
        new Vector2Int(1, 1),
        new Vector2Int(1, 0), //east
        new Vector2Int(1, -1),
        new Vector2Int(0, -1), //south
        new Vector2Int(-1, -1),
        new Vector2Int(-1, 0), //west
        new Vector2Int(-1, 1),
    };

    internal Board()
    {
        _grid = new Othello[_sideLength, _sideLength];
    }

    internal void SetOthello(Vector2Int position, OthelloColor color)
    {
        _grid[position.x, position.y].Generate(color);
        UpdatePuttablePosition(position);
    }

    internal void ChangeColor(Vector2Int position)
    {
        _grid[position.x, position.y].ChangeColor();
    }

    internal bool HasOthello(Vector2Int position)
    {
        if (!IsInGrid(position)) return false;
        if (_grid[position.x, position.y].Color == OthelloColor.None) return false;
        return true;
    }

    internal List<Vector2Int> GetPuttableGrid(OthelloColor turnColor)
    {
        if (_setCandidates.Count == 0) return null;

        List<Vector2Int> puttablePositions = new List<Vector2Int>();
        foreach (Vector2Int candidate in _setCandidates)
        {
            for (int i = 0; i < _sideLength; i++)
            {
                Vector2Int pos = candidate;
                bool canNotOut = true;
                for (int j = 1; canNotOut; j++) // Maybe, this is Not good algorithm.
                {
                    pos += j * _direction[i];
                    if (!HasOthello(pos)) break;

                    bool isSame = _grid[pos.x, pos.y].Color == turnColor;
                    if (j == 1 && !isSame) break;
                    if (j != 1 && !isSame) continue;

                    puttablePositions.Add(candidate);
                    canNotOut = false;
                }

                if (!canNotOut) break;
            }
        }

        return puttablePositions; // Completed?
    }

    internal void GetInitialized()
    {
        _isStarted = true;
    }

    void UpdatePuttablePosition(Vector2Int position)
    {
        if (!_isStarted) return;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++) // 9 times loop
            {
                if (i == 0 && j == 0) continue;

                Vector2Int researchPosition = new Vector2Int(position.x, position.y);
                if (IsInGrid(researchPosition)) continue;
                if (HasOthello(researchPosition)) continue;

                if (_setCandidates.Contains(researchPosition)) continue;

                _setCandidates.Add(researchPosition);
            }
        }
    }

    bool IsInGrid(Vector2Int position)
    {
        bool isInX = 0 <= position.x && position.x < _sideLength;
        bool isInY = 0 <= position.y && position.y < _sideLength;

        return isInX && isInY;
    }
}
