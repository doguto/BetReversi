using UnityEngine;
using UniRx;
using DG.Tweening;
using Project.Reversi.Model;
using Project.Reversi.Presenter;
using Project.Common.Model;
using Project.Common.Presenter;

namespace Project.Reversi.View
{
    public class OthelloView : MonoBehaviour
    {
        readonly Vector3 RotateVector = new(0, 180, 0);
        readonly float RotateTime = 0.5f;

        OthelloPresenter _presenter;
        Transform _transform;
        bool _canSubscribe;


        internal void Init(OthelloPresenter presenter, int othelloAmount = 1)
        {
            _transform = transform;
            _canSubscribe = false;
            _presenter = presenter;

            _presenter.Color.Subscribe((color) =>
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
}
