using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Othello
{
    internal OthelloColor Color { get; private set; } = OthelloColor.None;

    internal void Generate(OthelloColor color)
    {
        if (color == OthelloColor.None) return;

        Color = color;
    }

    internal void ChangeColor() 
    {
        if (Color == OthelloColor.None) return;
        if (Color == OthelloColor.white)
        {
            Color = OthelloColor.black;
        }
        else
        {
            Color = OthelloColor.white;
        }
    }
}

public enum OthelloColor
{
    None,
    white,
    black
}
