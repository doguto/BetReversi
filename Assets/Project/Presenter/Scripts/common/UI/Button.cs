using UniRx;
using System;


public class ButtonPresenterBase
{
    // protected ButtonModelBase model;
}


public class CheckButtonPresenter : ButtonPresenterBase
{
    CheckButtonModel _model;
    bool _isChecked = false;
    public bool IsChecked
    { 
        get
        {
            return _isChecked;
        }
        set
        {
            _isChecked = value;
            _model.isChecked = value;
        } 
    }

    internal CheckButtonPresenter(CheckButtonModel model)
    {
        _model = model;
    }
}


public class UpDownButtonPresenter : ButtonPresenterBase
{
    Subject<bool> _destroyer = new Subject<bool>();
    public IObservable<bool> Destroyer => _destroyer;

    UpDownButtonModel _model;
    int _value = 0;
    public int Value 
    { 
        get 
        {
            return _value;
        } 
        set 
        {
            _value = value;
            _model.Value = value;
        }
    }

    internal UpDownButtonPresenter(UpDownButtonModel model)
    {
        _model = model;
        model.Destroyer.Subscribe(_ => _destroyer.OnNext(true));
    }
}
