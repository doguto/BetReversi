using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using Project.Reversi.Model;

namespace Project.Test
{
    public class Tests : MonoBehaviour
    {
        [SetUp] 
        public void SetUp()
        {
            Debug.Log("Test SetUp");
            ReversiModel.InitializeReversi(OthelloColor.black, 32, true);
        }

        [Test]
        public void TestGetCandidates() // => ok. 
        {
            BoardModel board = new BoardModel();
            Initialize(board);

            List<Vector2Int> candidates = board.GetPuttableGrid(OthelloColor.black);

            if (candidates.Count == 0)
            {
                Debug.Log("No Puttable position");
                return;
            }

            Debug.Log("candidates are");
            foreach (var candidate in candidates) 
            {
                Debug.Log(candidate);
            }
        }

        [Test]
        public void TestChangeColor() // => ok.
        {
            BoardModel board = new BoardModel();
            Initialize(board);

            board.SetOthello(new Vector2Int(4, 2), OthelloColor.black);

            List<Vector2Int> changeOhtellos = new List<Vector2Int>();
            changeOhtellos = board.GetChangeOthello(new Vector2Int(4, 2), OthelloColor.black);
            foreach (Vector2Int pos in changeOhtellos)
            {
                Debug.Log(pos);
            }
        }

        [Test]
        public void TestShowResult() 
        {
            List<Vector2Int> positions = new List<Vector2Int>();
            positions.Add(new Vector2Int(3, 5));
            positions.Add(new Vector2Int(2, 3));

            int i = 1;
            foreach (var pos in positions)
            {
                ReversiModel.SetOthello(pos, 2 * i);
                i++;
            }

            // ReversiModel.ShowResult();
        }

        [TearDown]
        public void TearDown()
        {
            Debug.Log("Test TearDown");
        }

        void Initialize(BoardModel board)
        {
            board.SetOthello(new Vector2Int(3, 3), OthelloColor.black);
            board.SetOthello(new Vector2Int(3, 4), OthelloColor.white);
            board.SetOthello(new Vector2Int(4, 3), OthelloColor.white);
            board.SetOthello(new Vector2Int(4, 4), OthelloColor.black);
        }
    }
}