using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Othello
{
    public OthelloColor Color { get; private set; } = OthelloColor.None;

    public void Generate(OthelloColor color)
    {
        if (color == OthelloColor.None) return;

        Color = color;
    }

    public void ChangeColor(OthelloColor color) 
    {
        if (Color == color) return;

        Color = color;
    }
}

public enum OthelloColor
{
    None,
    white,
    black
}
