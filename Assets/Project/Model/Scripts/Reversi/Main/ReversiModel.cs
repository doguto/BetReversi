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
    private static bool _isStarted = false;
    private static bool _canNotSet = false;

    private static List<Vector2Int> _puttableGrids = new List<Vector2Int>();

    private static Board _board;
    private static ReversiPlayer _player;

    private static Subject<SetOthelloMessage> _setOthelloMessage = new Subject<SetOthelloMessage>();
    private static Subject<ChangeColorMessage> _changeColorMessage = new Subject<ChangeColorMessage>();

    public static IObservable<SetOthelloMessage> SetOthelloMessage => _setOthelloMessage;
    public static IObservable<ChangeColorMessage> ChangeColorMessage => _changeColorMessage;

    static ReversiModel()
    {
        _board = new Board();
    }

    public static void InitializeReversi(OthelloColor color, int othelloAmount)
    {
        _player = new ReversiPlayer(color, othelloAmount);

        SetOthello(new Vector2Int(3, 3), _black);
        SetOthello(new Vector2Int(3, 4), _white);
        SetOthello(new Vector2Int(4, 3), _white);
        SetOthello(new Vector2Int(4, 4), _black);

        _board.GetInitialized();
        _currentTurn = _firstTurn;
        _puttableGrids = _board.GetPuttableGrid(_firstTurn);
        _isStarted = true;
    }

    public static void SetPlayerOthello(Vector2Int position)
    {
        if (_player.PlayerColor != _currentTurn) return;

        SetOthello(position, _currentTurn);
    }

    internal static void SetOthello(Vector2Int position)
    {
        SetOthello(position, _currentTurn);
    }

    public static void SetOthello(Vector2Int position, OthelloColor color)
    {
        if (_board.HasOthello(position)) return;
        if (_isStarted && !_puttableGrids.Contains(position)) return;

        var message = new SetOthelloMessage(position, color);
        _board.SetOthello(position, color);
        _setOthelloMessage.OnNext(message);

        List<Vector2Int> changeOhtellos = new List<Vector2Int>();
        changeOhtellos = _board.GetChangeOthello(position, color);
        if (changeOhtellos.Count == 0) return;

        foreach (Vector2Int pos in changeOhtellos)
        {
            ChangeOthelloColor(pos);
        }
        ChangeTurn();
    }

    internal static void ChangeOthelloColor(Vector2Int position)
    {
        if (!_board.HasOthello(position)) return;

        var message = new ChangeColorMessage(position);
        _board.ChangeColor(position);
        _changeColorMessage.OnNext(message);
    }

    static void ChangeTurn()
    {
        _currentTurn = (_currentTurn == _white)? _black : _white;

        _puttableGrids = _board.GetPuttableGrid(_currentTurn);
        if (_puttableGrids.Count == 0)
        {
            Debug.Log("Player '" + _currentTurn + "' can't put any othello.");
            if (_canNotSet)
            {
                EndRevesi();
                return;
            }

            _canNotSet = true;
            ChangeTurn();
            return;
        }
        _canNotSet = false;

        if (_currentTurn == _player.PlayerColor) return;

        // wait a NPC Input.
        if (_puttableGrids.Count == 0)
        {
            ChangeTurn();
            return;
        }
        Debug.Log("NPC's turn");
        NPC.SetRandomPosition(_puttableGrids);
    }

    public static void EndRevesi()
    {
        Debug.Log("--- --- --- --- ---");
        Debug.Log("Reversi is Over!!");
        Debug.Log("--- --- --- --- ---");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
#else
    Application.Quit();//�Q�[���v���C�I��
#endif
    }
}
