using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class UpDownButton : MonoBehaviour
{
    [SerializeField] CountButton _upButton;
    [SerializeField] CountButton _downButton;

    UpDownButtonPresenter _presenter;
    [SerializeField] Text _text;
    
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
            _presenter.Value = value;
        }
    }

    public void Init(UpDownButtonPresenter presenter, int value = 0, int min = 0, int max = 0)
    {
        _presenter = presenter;
        Value = value;
        _max = max;
        _min = min;

        _presenter.Destroyer.Subscribe(_ => Delete());
        _upButton.Count.Subscribe(_ => Value++);
        _downButton.Count.Subscribe(_ => Value--);
    }

    public void ShowValue()
    {
        _text.text = _value.ToString();
    }
    
    internal int GetValue()
    {
        // _value += _upButton.Count.Value - _downButton.Count.Value;
        return Mathf.Clamp(_value, _min, _max);
    }

    public void Delete()
    {
        Destroy(this.gameObject);
    } 

}
