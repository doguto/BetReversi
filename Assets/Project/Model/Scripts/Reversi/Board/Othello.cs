using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Othello
{
    internal OthelloColor Color { get; private set; } = OthelloColor.None;

    internal int Amount { get; private set; }

    internal void Generate(OthelloColor color, int amount)
    {
        if (color == ReversiModel.None) return;
        Color = color;
        Amount = amount;
    }

    internal void ChangeColor() 
    {
        if (Color == ReversiModel.None) return;
        Color = (Color == ReversiModel.White)? ReversiModel.Black : ReversiModel.White;
    }
}

public enum OthelloColor
{
    None,
    white,
    black
}
