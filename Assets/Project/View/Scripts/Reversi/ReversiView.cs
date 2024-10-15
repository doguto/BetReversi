using UnityEngine;
using UniRx;

public class ReversiView : MonoBehaviour
{
    [SerializeField] GameObject _othelloPrefab; // Up is black.
    ReversiPresenter _presenter;

    readonly Vector3 _whiteInit = new Vector3(0, 180, 0);

    private void Start()
    {
        _presenter = new ReversiPresenter();
        _presenter.othelloPresenters.ObserveAdd().Subscribe((presenter) =>
        {
            var othello = Instantiate(_othelloPrefab);
            othello.GetComponent<OthelloView>().Init(presenter.Value);
            if (presenter.Value.color.Value == OthelloColor.white)
            {
                othello.transform.localEulerAngles = _whiteInit;
            }
        });
    }

}
