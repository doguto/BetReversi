using UnityEngine;
using UniRx;

namespace Project.Common.View
{
    public class CountButton : ButtonViewBase
    {
        int _max = 0;
        int _min = 0;
        public ReactiveProperty<int> Count { get; private set; } = new (0);

        protected void Init(int max = 0, int min = 0)
        {
            _max = max;
            _min = min;
        }

        protected override void OnMouseDown()
        {
            Count.Value++;
            if (_max == _min) return;
            Mathf.Clamp(Count.Value, _min, _max);
        }
    }
}
