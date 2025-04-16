using UnityEngine;
using UniRx;
using System;
using Project.Reversi.Model;
using Project.Common.Presenter;

namespace Project.Reversi.Presenter
{
    public class ReversiPresenter
    {
        readonly Vector3 upDownButtonLeftPosition = new(-1.7f, 0, 0);
        readonly Vector3 checkButtonLeftPosition = new(-0.8f, 0, 0);
        readonly Vector3 upDownButtonRightPosition = new(0.8f, 0, 0);
        readonly Vector3 checkButtonRightPosition = new(1.7f, 0, 0);

        public ReactiveCollection<OthelloPresenter> OthelloPresenters { get; private set; } = new();
        Subject<ResultMessage> _resultSubject = new();
        public IObservable<ResultMessage> ResultSubject => _resultSubject;

        public ButtonBuilder ButtonBuilder { get; private set; } = new();

        public ReversiPresenter()
        {
            ReversiModel.SetOthelloMessage.Subscribe(SetOthello);
            ReversiModel.ChangeColorMessage.Subscribe(ChangeOthelloColor);
            ReversiModel.ResultMessage.Subscribe(ShowResult);
        }

        public void InitializeReversi(OthelloColor playerColor, int othelloAmount, bool isSoloGame)
        {
            ReversiModel.InitializeReversi(playerColor, othelloAmount, isSoloGame);
        }

        void SetOthello(SetOthelloMessage message)
        {
            var presenter = new OthelloPresenter(message.Position, message.Color, message.ByPlayer);
            OthelloPresenters.Add(presenter);
            if (!message.ByPlayer) return;
            
            var checkButtonPresenter = new CheckButtonPresenter(message.ConfirmButtonModel);
            var upDownButtonPresenter = new UpDownButtonPresenter(message.UpDownButtonModel);
            var position = new Vector3(message.Position.x, message.Position.y, 0);
            ButtonBuilder.BuildNewButton(checkButtonPresenter, position + checkButtonRightPosition, position + checkButtonLeftPosition);
            ButtonBuilder.BuildNewButton(upDownButtonPresenter, position + upDownButtonRightPosition, position + upDownButtonLeftPosition);
        }

        void ChangeOthelloColor(ChangeColorMessage message)
        {
            foreach (var presenter in OthelloPresenters)
            {
                if (presenter.Position != message.Position) continue;

                presenter.ChangeColor();
                break;
            }
        }

        void ShowResult(ResultMessage message)
        {
            _resultSubject.OnNext(message);
        }
    }
}