using UnityEngine;
using UniRx;
using Project.Common.Model;

namespace Project.Common.Presenter
{
    public class UIBuilder
    {
        
    }


    public class ButtonBuilder
    {
        public ReactiveCollection<ButtonInfo> _buttonPresenters = new ReactiveCollection<ButtonInfo>();

        internal void BuildNewButton(ButtonPresenterBase button, Vector3 position, Vector3 sparePosition = default)
        {
            ButtonInfo buttonInfo = new ButtonInfo(button, position, sparePosition);
            _buttonPresenters.Add(buttonInfo);
        }
    }

    public class ButtonInfo
    {
        public ButtonPresenterBase ButtonPresenter { get; internal set; }
        public Vector3 Position { get; internal set; }
        public Vector3 SparePosition { get; internal set; }

        internal ButtonInfo(ButtonPresenterBase buttonPresenter, Vector3 position, Vector3 sparePosition = default)
        {
            ButtonPresenter = buttonPresenter;
            Position = position;
            SparePosition = sparePosition;
        }
    }
}
