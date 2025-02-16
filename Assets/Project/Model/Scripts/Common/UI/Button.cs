using UniRx;
using System;

public class ButtonModelBase  // 要修正。Genericの必要無し
{
    // Subject<T> subject = new Subject<T>();
    // public IObservable<T> Subject => subject;
}


public class CheckButtonModel : ButtonModelBase
{
    public bool isChecked = false;

    internal CheckButtonModel()
    {
        isChecked = false; 
    }
}


public class UpDownButtonModel : ButtonModelBase
{
    Subject<bool> _destroyer = new Subject<bool>();
    public IObservable<bool> Destroyer => _destroyer;

    int _value = 0;
    public int Value 
    { 
        get
        {
            _destroyer.OnNext(true);
            return _value;
        } 
        set
        {
            _value = value;
        } 
    }

    internal UpDownButtonModel()
    {
        _value = 0;
    }
}