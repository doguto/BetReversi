using System;
using UnityEngine;
using UniRx;

public class BoardPresenter
{
    private Subject<Vector2Int> _mouseInput;
    private Subject<Vector2Int> _opponentInput;
    public IObserver<Vector2Int> MouseInput => _mouseInput;
    public IObserver<Vector2Int> OpponentInput => _opponentInput;


    public BoardPresenter()
    {
        _mouseInput = new Subject<Vector2Int>();
        _opponentInput = new Subject<Vector2Int>();
        _mouseInput.Subscribe((pos) =>
        {
            ReversiModel.SetPlayerOthello(pos);
        });
        _opponentInput.Subscribe((pos) =>
        {
            Vector2Int transedPos = new Vector2Int(8 - pos.x, 8 - pos.y);
            ReversiModel.SetOpponentOthello(transedPos);
        });
    }
}
