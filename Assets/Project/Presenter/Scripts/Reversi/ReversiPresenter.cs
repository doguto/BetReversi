using UnityEngine;
using UniRx;

public class ReversiPresenter
{
    public ReactiveCollection<OthelloPresenter> OthelloPresenters { get; private set; } = new ReactiveCollection<OthelloPresenter>();

    public ReversiPresenter()
    {
        ReversiModel.SetOthelloMessage.Subscribe(SetOthello);
        ReversiModel.ChangeColorMessage.Subscribe(ChangeOthelloColor);
        //ReversiModel.InitializeReversi();
    }

    public void InitializeReversi(OthelloColor playerColor, int othelloAmount)
    {
        ReversiModel.InitializeReversi(playerColor, othelloAmount);
    }

    void SetOthello(SetOthelloMessage message)
    {
        OthelloPresenter presenter = new OthelloPresenter(message.Position, message.Color);
        OthelloPresenters.Add(presenter);
    }

    void ChangeOthelloColor(ChangeColorMessage message)
    {
        foreach (OthelloPresenter presenter in OthelloPresenters)
        {
            if (presenter.position != message.Position) continue;

            presenter.ChangeColor();
            break;
        }
    }
}
