using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOthelloMessage
{
    public Vector2Int Position { get; private set; }
    public OthelloColor Color { get; private set; }
    public bool Byplayer { get; private set; }
    public CheckButtonModel ConfirmButtonModel { get; private set; }
    public UpDownButtonModel UpDownButtonModel { get; private set; }

    internal SetOthelloMessage(Vector2Int position, OthelloColor color, bool byPlayer = false)
    {
        Position = position;
        Color = color;
        Byplayer = byPlayer;
    }

    internal SetOthelloMessage(Vector2Int position, OthelloColor color, CheckButtonModel confirmButtonModel, UpDownButtonModel upDownButtonModel)
    {
        Position = position;
        Color = color;
        Byplayer = true;
        ConfirmButtonModel = confirmButtonModel;
        UpDownButtonModel = upDownButtonModel;
    }
}
