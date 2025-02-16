using UnityEngine;
using UniRx;

public class CountButton : ButtonViewBase
{
    int _max;
    int _min;
    public ReactiveProperty<int> Count { get; private set; } = new ReactiveProperty<int>(0);

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
