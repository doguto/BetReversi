using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class Othello
{
    readonly OthelloColor _white = OthelloColor.white;
    readonly OthelloColor _black = OthelloColor.black;
    readonly OthelloColor _none = OthelloColor.None;
    internal OthelloColor Color { get; private set; } = OthelloColor.None;

    internal void Generate(OthelloColor color)
    {
        if (color == _none) return;

        Color = color;
    }

    internal void ChangeColor() 
    {
        if (Color == _none) return;

        Color = (Color == _white)? _black : _white;
    }
}

public enum OthelloColor
{
    None,
    white,
    black
}
