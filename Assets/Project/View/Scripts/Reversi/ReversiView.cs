using System;
using UnityEngine;
using UniRx;

public class ReversiView : MonoBehaviour
{
    [SerializeField] GameObject _othelloPrefab; // Up is black.
    ReversiPresenter _presenter;

    readonly Vector3 _whiteAngle = new Vector3(0, 180, 0);

    private void Start()
    {
        InitializeReversi(OthelloColor.black, 32);
    }

    internal void InitializeReversi(OthelloColor playerColor, int othelloAmount)
    {
        _presenter = new ReversiPresenter();
        _presenter.OthelloPresenters.ObserveAdd().Subscribe((presenter) =>
        {
            //Debug.Log("subscrive " + presenter.Value.color.Value);
            var othello = Instantiate(_othelloPrefab);
            if (presenter.Value.color.Value == OthelloColor.white)
            {
                othello.transform.localEulerAngles = _whiteAngle;
            }
            othello.GetComponent<OthelloView>().Init(presenter.Value);
        });
        //Debug.Log("initialize Reversi");
        _presenter.InitializeReversi(playerColor, othelloAmount);
    }
}
