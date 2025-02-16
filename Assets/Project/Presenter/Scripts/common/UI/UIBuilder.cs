using UnityEngine;
using UniRx;

public class UIBuilder
{

}


public class ButtonBuilder
{
    public ReactiveCollection<ButtonInfo> _buttonPresenters = new ReactiveCollection<ButtonInfo>();

    internal void BuildNewButton(ButtonPresenterBase button, Vector3 position)
    {
        ButtonInfo buttonInfo = new ButtonInfo(button, position);
        _buttonPresenters.Add(buttonInfo);
    }
}

public class ButtonInfo
{
    public ButtonPresenterBase ButtonPresenter { get; internal set; }
    public Vector3 Position { get; internal set; }
    internal ButtonInfo(ButtonPresenterBase buttonPresenter, Vector3 position)
    {
        this.ButtonPresenter = buttonPresenter;
        Position = position;
    }
}
