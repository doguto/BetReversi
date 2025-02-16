using UnityEngine;
using UniRx;
using DG.Tweening;

public class OthelloView : MonoBehaviour
{
    readonly Vector3 RotateVector = new Vector3(0, 180, 0);
    readonly float RotateTime = 0.5f;

    OthelloPresenter _presenter;
    Transform _transform;
    bool _canSubscribe;


    internal void Init(OthelloPresenter presenter, int othelloAmount = 1)
    {
        _transform = transform;
        _canSubscribe = false;
        _presenter = presenter;

        _presenter.color.Subscribe((color) =>
        {
            if (!_canSubscribe)
            {
                _canSubscribe = true;
                return;
            }

            _transform.DOLocalRotate(_transform.localEulerAngles + RotateVector, RotateTime);
        });
    }
}
