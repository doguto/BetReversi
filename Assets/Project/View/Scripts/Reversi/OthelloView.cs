using UnityEngine;
using UniRx;
using DG.Tweening;

public class OthelloView : MonoBehaviour
{
    OthelloPresenter _presenter;
    Transform _transform;

    readonly Vector3 _rotateVector = new Vector3(0, 180, 0);
    readonly float _rotateTime;


    internal void Init(OthelloPresenter presenter)
    {
        _transform = transform;
        _transform.position = new Vector3 (presenter.position.x, presenter.position.y, 0);

        _presenter = presenter;
        _presenter.color.Subscribe((color) =>
        {
            _transform.DOLocalRotate(_rotateVector, _rotateTime);
        });
    }
}
