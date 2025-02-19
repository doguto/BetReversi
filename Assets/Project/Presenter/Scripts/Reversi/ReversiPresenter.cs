using UnityEngine;
using UniRx;

public class ReversiPresenter
{
    readonly Vector3 UpDownButtonLeftPosition = new Vector3(-1.7f, 0, 0);
    readonly Vector3 CheckButtonLeftPosition = new Vector3(-0.8f, 0, 0);
    readonly Vector3 UpDownButtonRightPosition = new Vector3(0.8f, 0, 0);
    readonly Vector3 CheckButtonRightPosition = new Vector3(1.7f, 0, 0);

    public ReactiveCollection<OthelloPresenter> OthelloPresenters { get; private set; } = new ReactiveCollection<OthelloPresenter>();
    // public Subject<bool> PlayersSet { get; private set; } = new Subject<bool>(); 
    public ButtonBuilder ButtonBuilder { get; private set; } = new ButtonBuilder();

    public ReversiPresenter()
    {
        ReversiModel.SetOthelloMessage.Subscribe(SetOthello);
        ReversiModel.ChangeColorMessage.Subscribe(ChangeOthelloColor);
    }

    public void InitializeReversi(OthelloColor playerColor, int othelloAmount, bool isSoloGame)
    {
        ReversiModel.InitializeReversi(playerColor, othelloAmount, isSoloGame);
    }

    void SetOthello(SetOthelloMessage message)
    {
        OthelloPresenter presenter = new OthelloPresenter(message.Position, message.Color, message.Byplayer);
        OthelloPresenters.Add(presenter);
        if (!message.Byplayer) return;
        
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
}
