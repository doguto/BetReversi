using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using System;

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
        board.SetOthello(new Vector2Int(3, 3), OthelloColor.black);
        board.SetOthello(new Vector2Int(3, 4), OthelloColor.white);
        board.SetOthello(new Vector2Int(4, 3), OthelloColor.white);
        board.SetOthello(new Vector2Int(4, 4), OthelloColor.black);


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

    [TearDown]
    public void TearDown()
    {
        Debug.Log("Test TearDown");
    }
}