using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public static class ReversiModel
{
    private static readonly OthelloColor _white = OthelloColor.white;
    private static readonly OthelloColor _black = OthelloColor.black;

    private static readonly OthelloColor _firstTurn = OthelloColor.black;
    private static OthelloColor _currentTurn = OthelloColor.None;

    private static Board _board;

    static Subject<SetOthelloMessage> _setOthelloMessage = new Subject<SetOthelloMessage>();
    static Subject<ChangeColorMessage> _changeColorMessage = new Subject<ChangeColorMessage>();

    public static IObservable<SetOthelloMessage> SetOthelloMessage => _setOthelloMessage;
    public static IObservable<ChangeColorMessage> ChangeColorMessage => _changeColorMessage;

    static ReversiModel()
    {
        _board = new Board();

        SetOthello(new Vector2Int(3, 3), _black);
        SetOthello(new Vector2Int(3, 4), _white);
        SetOthello(new Vector2Int(4, 3), _white);
        SetOthello(new Vector2Int(4, 4), _black);

        _board.GetInitialized();
        _currentTurn = _firstTurn;
    }

    public static void SetOthello(Vector2Int position)
    {
        SetOthello(position, _currentTurn);
    }

    public static void SetOthello(Vector2Int position, OthelloColor color)
    {
        if (_board.HasOthello(position)) return;

        var candidates = _board.GetPuttableGrid(color);
        if (!candidates.Contains(position)) return;

        var message = new SetOthelloMessage(position, color);
        _board.SetOthello(position, color);
        _setOthelloMessage.OnNext(message);

        List<Vector2Int> changeOhtellos = new List<Vector2Int>();
        changeOhtellos = _board.GetChangeOthello(position, color);
        foreach (Vector2Int pos in changeOhtellos)
        {
            ChangeOthelloColor(pos);
        }

        ChangeTurn();
    }

    internal static void ChangeOthelloColor(Vector2Int position)
    {
        if (_board.HasOthello(position)) return;

        var message = new ChangeColorMessage(position);
        _board.ChangeColor(position);
        _changeColorMessage.OnNext(message);
    }

    static void ChangeTurn()
    {
        if (_currentTurn == _white)
        {
            _currentTurn = _black;  
        }
        else if (_currentTurn == _black)
        {
            _currentTurn = _white;
        }
    }
}
