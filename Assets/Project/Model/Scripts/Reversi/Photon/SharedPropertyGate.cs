using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using System.Globalization;

public static class SharedPropertyGate
{
    private const string _setOthelloPositionKey = "OthelloPosition";

    private static readonly Hashtable _setOthelloPosition = new Hashtable();

    public static Vector2Int GetSetOthelloPosition(this Player player)
    {
        return (player.CustomProperties[_setOthelloPositionKey] is Vector2Int setOthelloPosition)? 
            setOthelloPosition : new Vector2Int(-1, -1);
    }
}
