using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ReversiPlayer
{
    readonly int _minOthelloAmount = 0;
    readonly OthelloColor _playerColor;

    internal int CurrentOthelloAmount { get; private set; }
    
    internal ReversiPlayer(OthelloColor color, int othelloAmount)
    {
        _playerColor = color;
        CurrentOthelloAmount = othelloAmount;
    }

    internal void UseOthello(int usedAmount)
    {
        if (CurrentOthelloAmount - usedAmount < _minOthelloAmount)
        {
            Debug.LogError("Don't use Othello over an amount you have.");
            return;
        }

        CurrentOthelloAmount -= usedAmount;
    }
}
