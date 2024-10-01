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

    internal void ChangeColor(OthelloColor color) 
    {
        if (Color == color) return;

        Color = color;
    }
}

internal enum OthelloColor
{
    None,
    white,
    black
}
