using UnityEngine;
using UniRx;
using System;
using Project.Reversi.Model;
using Project.Common.Presenter;

namespace Project.Reversi.Presenter
{
    public class ReversiPresenter
    {
        readonly Vector3 UpDownButtonLeftPosition = new(-1.7f, 0, 0);
        readonly Vector3 CheckButtonLeftPosition = new(-0.8f, 0, 0);
        readonly Vector3 UpDownButtonRightPosition = new(0.8f, 0, 0);
        readonly Vector3 CheckButtonRightPosition = new(1.7f, 0, 0);

        public ReactiveCollection<OthelloPresenter> OthelloPresenters { get; private set; } = new();
        private Subject<ResultMessage> _resultSubject = new();
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
            OthelloPresenter presenter = new OthelloPresenter(message.Position, message.Color, message.ByPlayer);
            OthelloPresenters.Add(presenter);
            if (!message.ByPlayer) return;
            
            CheckButtonPresenter checkButtonPresenter = new CheckButtonPresenter(message.ConfirmButtonModel);
            UpDownButtonPresenter upDownButtonPresenter = new UpDownButtonPresenter(message.UpDownButtonModel);
            Vector3 position = new Vector3(message.Position.x, message.Position.y, 0);
            ButtonBuilder.BuildNewButton(checkButtonPresenter, position + CheckButtonRightPosition, position + CheckButtonLeftPosition);
            ButtonBuilder.BuildNewButton(upDownButtonPresenter, position + UpDownButtonRightPosition, position + UpDownButtonLeftPosition);
        }

        void ChangeOthelloColor(ChangeColorMessage message)
        {
            foreach (OthelloPresenter presenter in OthelloPresenters)
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