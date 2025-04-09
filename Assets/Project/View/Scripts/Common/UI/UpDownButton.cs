using UnityEngine;
using UniRx;
using TMPro;
using Project.Common.Presenter;

namespace Project.Common.View
{
    public class UpDownButton : MonoBehaviour
    {
        [SerializeField] CountButton _upButton;
        [SerializeField] CountButton _downButton;

        UpDownButtonPresenter _presenter;
        // [SerializeField] Text _text;
        [SerializeField] TextMeshProUGUI _text;
        
        int _max;
        int _min;
        int _value;
        public int Value
        { 
            get
            {
                return _value;
            } 
            set
            {
                _value = value;
                Mathf.Clamp(_value, _min, _max);
                _presenter.Value = value;
                ShowValue();
            }
        }

        public void Init(UpDownButtonPresenter presenter, int value = 0, int min = 0, int max = 0)
        {
            _presenter = presenter;
            Value = value;
            _max = max;
            _min = min;
            ShowValue();

            _presenter.Destroyer.Subscribe(_ => Destroy());
            _upButton.Count.Subscribe(_ => Value++);
            _downButton.Count.Subscribe(_ => Value--);
        }

        public void ShowValue()
        {
            _text.text = _value.ToString();
        }
        
        // internal int GetValue()
        // {
        //     return Mathf.Clamp(_value, _min, _max);
        // }

        public void Destroy()
        {
            Destroy(this.gameObject);
        } 

    }
}
