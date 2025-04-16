using System.Collections.Generic;
using UnityEngine;

namespace Project.Reversi.Model
{
    internal class BoardModel
    {
        readonly OthelloModel[,] grid;
        readonly Vector2Int[] direction =
        {
            new(0, 1), //north
            new(1, 1),
            new(1, 0), //east
            new(1, -1),
            new(0, -1), //south
            new(-1, -1),
            new(-1, 0), //west
            new(-1, 1),
        };

        readonly List<Vector2Int> setCandidates = new();
        internal List<Vector2Int> SettablePositions { get; private set; }

        bool _isStarted;
        

        internal BoardModel()
        {
            grid = new OthelloModel[ReversiModel.Length, ReversiModel.Length];
            for (int x = 0; x < ReversiModel.Length; x++)
            {
                for (int y = 0; y < ReversiModel.Length; y++)
                {
                    grid[x, y] = new OthelloModel();
                }
            }
        }

        internal void Initialize()
        {
            _isStarted = true;
        }

        internal void SetOthello(Vector2Int position, OthelloColor color, int betAmount = 1)
        {
            grid[position.x, position.y].Generate(color, betAmount); // later , need to make codes for othello amount.
            setCandidates.Remove(position);
            UpdateSetCandidate(position);
        }

        internal void ChangeColor(Vector2Int position)
        {
            grid[position.x, position.y].ChangeColor();
        }

        internal bool HasOthello(Vector2Int position)
        {
            if (!IsInGrid(position)) return false;
            if (grid[position.x, position.y].Color == OthelloColor.None) return false;
            return true;
        }

        internal List<Vector2Int> GetPuttableGrid(OthelloColor turnColor)
        {
            var puttablePositions = new List<Vector2Int>();

            if (setCandidates.Count == 0) return puttablePositions;

            // Maybe, this is Not good algorithm.
            foreach (Vector2Int candidate in setCandidates)
            {
                for (int i = 0; i < direction.Length; i++)
                {
                    var pos = candidate;
                    var canOut = false;
                    for (int j = 1; j < ReversiModel.Length; j++)
                    {   
                        pos += direction[i];
                        if (!HasOthello(pos)) break;

                        var isSame = grid[pos.x, pos.y].Color == turnColor;
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

            for (int i = 0; i < direction.Length; i++)
            {
                Vector2Int pos = putPosition;
                for (int j = 1; j < 8; j++)
                {
                    pos += direction[i];
                    if (!IsInGrid(pos)) break;
                    if (!HasOthello(pos)) break;

                    bool isSame = grid[pos.x, pos.y].Color == putColor;
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
                        Vector2Int change = putPosition + k * direction[i];
                        changeGrids.Add(change);
                    }
                    break;
                }
            }

            return changeGrids;
        }

        internal int GetOthelloAmount(OthelloColor color)
        {
            var amount = 0;
            for (int y = 0; y < ReversiModel.Length; y++)
            {
                for (int x = 0; x < ReversiModel.Length; x++)
                {
                    OthelloColor gridColor = grid[x, y].Color;
                    if (gridColor != color) continue;

                    amount += grid[x, y].Amount;
                }
            }

            return amount;
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

                    if (setCandidates.Contains(researchPosition)) continue;

                    setCandidates.Add(researchPosition);
                }
            }
        }

        bool IsInGrid(Vector2Int position)
        {
            bool isInX = 0 <= position.x && position.x < ReversiModel.Length;
            bool isInY = 0 <= position.y && position.y < ReversiModel.Length;

            return isInX && isInY;
        }
    }
}