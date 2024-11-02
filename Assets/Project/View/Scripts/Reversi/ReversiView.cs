//using System;
using UnityEngine;
using UniRx;

public class ReversiView : MonoBehaviour
{
    [SerializeField] GameObject _othelloPrefab; // Up is black.
    ReversiPresenter _presenter;

    readonly Vector3 _whiteAngle = new Vector3(0, 180, 0);

    private void Start()
    {
        //InitializeReversi(OthelloColor.black, 32);
        InitializeTest();
    }

    internal void InitializeReversi(OthelloColor playerColor, int othelloAmount)
    {
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

        _presenter.InitializeReversi(playerColor, othelloAmount);
    }


    void InitializeTest()
    {
        OthelloColor[] colors = { OthelloColor.white, OthelloColor.black };
        int PlayerColorIndex = Random.Range(0, 2);
        OthelloColor playerColor = colors[PlayerColorIndex];
        InitializeReversi(playerColor, 32);
    }
}
