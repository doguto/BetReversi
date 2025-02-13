using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CountButton : ButtonBase
{
    int _max;
    int _min;
    public ReactiveProperty<int> Count { get; private set; }

    protected void Init(int max_ = 1, int min_ = 0)
    {
        _max = max_;
        _min = min_;
    }

    protected override void OnMouseDown()
    {
        Count.Value++;
        Mathf.Clamp(Count.Value, _min, _max);
    }
}
