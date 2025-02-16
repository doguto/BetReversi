using UnityEngine;
using System;
using UniRx;

public class UpDownButton : MonoBehaviour
{
    [SerializeField] CountButton _upButton;
    [SerializeField] CountButton _downButton;

    UpDownButtonPresenter _presenter;
    
    int _max;
    int _min;
    int _value;

    public void Init(int min, int max, UpDownButtonPresenter presenter)
    {
        _max = max;
        _min = min;
        _presenter = presenter;
        _presenter.Destroyer.Subscribe(_ => Delete());
    }
    
    internal int GetValue()
    {
        return Mathf.Clamp(_upButton.Count.Value - _downButton.Count.Value, _min, _max);
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    } 

}
