using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using System.Globalization;

public static class SharedPropertyGate
{
    const string SetOthelloPositionKey = "OthelloPosition";

    static readonly Hashtable SetOthelloPosition = new();

    public static Vector2Int GetSetOthelloPosition(this Player player)
    {
        return (player.CustomProperties[SetOthelloPositionKey] is Vector2Int setOthelloPosition)? 
            setOthelloPosition : new Vector2Int(-1, -1);
    }
}
