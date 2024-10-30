using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOthelloMessage
{
    public Vector2Int Position {  get; private set; }
    public OthelloColor Color { get; private set; }
    internal SetOthelloMessage(Vector2Int position, OthelloColor color)
    {
        Position = position;
        Color = color;
    }
}
