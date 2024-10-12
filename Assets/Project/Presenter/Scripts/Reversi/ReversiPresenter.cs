using UniRx;

public class ReversiPresenter
{
    public ReactiveCollection<OthelloPresenter> othelloPresenters { get; private set; }


    public ReversiPresenter()
    {
        ReversiModel.SetOthelloMessage.Subscribe(SetOthello);
        ReversiModel.ChangeColorMessage.Subscribe(ChangeOthelloColor);

        othelloPresenters = new ReactiveCollection<OthelloPresenter>();
    }

    void SetOthello(SetOthelloMessage message)
    {
        OthelloPresenter presenter = new OthelloPresenter(message.Position, message.Color);
        othelloPresenters.Add(presenter);
    }

    void ChangeOthelloColor(ChangeColorMessage message)
    {
        foreach (OthelloPresenter presenter in othelloPresenters)
        {
            if (presenter.position != message.Position) continue;

            presenter.ChangeColor();
            break;
        }
    }
}
