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
        for (int x = 0; x < _sideLength; x++)
        {
            for (int y = 0; y < _sideLength; y++)
            {
                _grid[x, y] = new Othello();
            }
        }
    }

    internal void GetInitialized()
    {
        _isStarted = true;
    }

    internal void SetOthello(Vector2Int position, OthelloColor color)
    {
        _grid[position.x, position.y].Generate(color);
        _setCandidates.Remove(position);
        UpdateSetCandidate(position);
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
        List<Vector2Int> puttablePositions = new List<Vector2Int>();

        if (_setCandidates.Count == 0)
            return puttablePositions;

        // Maybe, this is Not good algorithm.
        foreach (Vector2Int candidate in _setCandidates)
        {
            for (int i = 0; i < _direction.Length; i++)
            {
                Vector2Int pos = candidate;
                bool canOut = false;
                for (int j = 1; j < _sideLength; j++)
                {   
                    pos += _direction[i];
                    if (!HasOthello(pos)) break;

                    bool isSame = _grid[pos.x, pos.y].Color == turnColor;
                    if(j == 1)
                    {
                        if (isSame) break;
                        continue;
                    }

                    if (!isSame) continue;

                    puttablePositions.Add(candidate);
                    canOut = true;
                    break;
                }

                if (canOut) break;
            }
        }

        return puttablePositions; // Completed
    }

    internal List<Vector2Int> GetChangeOthello(Vector2Int putPosition, OthelloColor putColor)
    {
        List<Vector2Int> changeGrids = new List<Vector2Int>();

        if (!IsInGrid(putPosition)) return changeGrids;

        for (int i = 0; i < _direction.Length; i++)
        {
            Vector2Int pos = putPosition;
            for (int j = 1; j < 8; j++)
            {
                pos += _direction[i];
                if (!IsInGrid(pos)) break;
                if (!HasOthello(pos)) break;

                bool isSame = _grid[pos.x, pos.y].Color == putColor;
                if (j == 1)
                {
                    if (isSame) break;
                    continue;
                }
                if (!isSame) continue;

                // sameColor othello merges in first time.
                int lim = j;
                for (int k = 1; k < lim; k++)
                {
                    Vector2Int change = putPosition + k * _direction[i];
                    changeGrids.Add(change);
                }
                break;
            }
        }

        return changeGrids;
    }

    void UpdateSetCandidate(Vector2Int position)
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++) // 9 times loop
            {
                if (i == 0 && j == 0) continue;

                Vector2Int researchPosition = new Vector2Int(position.x + i, position.y + j);
                if (!IsInGrid(researchPosition)) continue;
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
