using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;
using UnityEngine.UIElements;

public class Tests : MonoBehaviour
{
    [SetUp] 
    public void SetUp()
    {
        Debug.Log("Test SetUp");
    }

    [Test]
    public void TestGetCandidates() // => ok. 
    {
        Board board = new Board();
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
        Board board = new Board();
        Initialize(board);

        board.SetOthello(new Vector2Int(4, 2), OthelloColor.black);

        List<Vector2Int> changeOhtellos = new List<Vector2Int>();
        changeOhtellos = board.GetChangeOthello(new Vector2Int(4, 2), OthelloColor.black);
        foreach (Vector2Int pos in changeOhtellos)
        {
            Debug.Log(pos);
        }
    }

    [TearDown]
    public void TearDown()
    {
        Debug.Log("Test TearDown");
    }

    void Initialize(Board board)
    {
        board.SetOthello(new Vector2Int(3, 3), OthelloColor.black);
        board.SetOthello(new Vector2Int(3, 4), OthelloColor.white);
        board.SetOthello(new Vector2Int(4, 3), OthelloColor.white);
        board.SetOthello(new Vector2Int(4, 4), OthelloColor.black);
    }
}