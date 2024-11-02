using UnityEngine;
using UniRx;
using DG.Tweening;

public class OthelloView : MonoBehaviour
{
    readonly Vector3 _rotateVector = new Vector3(0, 180, 0);
    readonly float _rotateTime = 0.5f;

    OthelloPresenter _presenter;
    Transform _transform;
    bool _canSubscribe;


    internal void Init(OthelloPresenter presenter)
    {
        _transform = transform;
        _transform.position = new Vector3 (presenter.position.x, presenter.position.y, 0);
        _canSubscribe = false;
        //Debug.Log(this.transform.localEulerAngles);

        _presenter = presenter;
        //Debug.Log("make subscirber.");
        _presenter.color.Subscribe((color) =>
        {
            if (!_canSubscribe)
            {
                _canSubscribe = true;
                return;
            }

            //Debug.Log("changed to " + color);
            _transform.DOLocalRotate(_transform.localEulerAngles + _rotateVector, _rotateTime);
            //Debug.Log(this.transform.eulerAngles);
        });
    }
}
