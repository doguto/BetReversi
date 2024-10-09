using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorMessage
{
    public Vector2Int Position {  get; private set; }
    public ChangeColorMessage(Vector2Int position)
    {
        Position = position;
    }
}
