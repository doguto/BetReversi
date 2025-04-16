using System;
using UnityEngine;
using UniRx;
using Project.Reversi.Model;

namespace Project.Reversi.Presenter
{
    public class BoardPresenter
    {
        readonly Subject<Vector2Int> mouseInput;
        readonly Subject<Vector2Int> opponentInput;
        public IObserver<Vector2Int> MouseInput => mouseInput;
        public IObserver<Vector2Int> OpponentInput => opponentInput;


        public BoardPresenter()
        {
            mouseInput = new Subject<Vector2Int>();
            opponentInput = new Subject<Vector2Int>();
            mouseInput.Subscribe((pos) => { ReversiModel.SetPlayerOthello(pos); });
            opponentInput.Subscribe((pos) =>
            {
                var transedPos = new Vector2Int(8 - pos.x, 8 - pos.y);
                ReversiModel.SetOpponentOthello(transedPos);
            });
        }
    }
}