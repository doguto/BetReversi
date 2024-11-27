using UnityEngine;
using UniRx;

public class ReversiView : MonoBehaviour
{
    [SerializeField] GameObject _othelloPrefab; // Up is black.
    [SerializeField] BoardView _boardView;

    ReversiPresenter _presenter;
    OthelloColor _playerColor = OthelloColor.white; // temp
    int _othelloAmount = 32; // temp
    bool _isSoloGame = true;

    readonly Vector3 _whiteAngle = new Vector3(0, 180, 0);

    private void Start()
    {
        //InitializeReversi(OthelloColor.black, 32);
        //InitializeTest();
    }

    internal void InitializeReversi()
    {
        InitializeReversi(true);
    }

    internal void InitializeReversi(bool isSoloGame)
    {
        _isSoloGame = isSoloGame;
        _boardView.InitializeBoard();

        if (!_isSoloGame) DecidePlayerColor();

        _presenter = new ReversiPresenter();
        _presenter.OthelloPresenters.ObserveAdd().Subscribe((presenter) =>
        {
            var othello = Instantiate(_othelloPrefab);
            if (presenter.Value.color.Value == OthelloColor.white)
            {
                othello.transform.localEulerAngles = _whiteAngle;
            }
            othello.GetComponent<OthelloView>().Init(presenter.Value);
        });

        _presenter.InitializeReversi(_playerColor, _othelloAmount, _isSoloGame);
    }

    void DecidePlayerColor()
    {
        OthelloColor[] colors = { OthelloColor.white, OthelloColor.black };
        int PlayerColorIndex = Random.Range(0, 2);
        _playerColor = colors[PlayerColorIndex];
    }
    

    void InitializeTest()
    {
        OthelloColor[] colors = { OthelloColor.white, OthelloColor.black };
        int PlayerColorIndex = Random.Range(0, 2);
        _playerColor = colors[PlayerColorIndex];
        _othelloAmount = 32;
        InitializeReversi();
    }
}
