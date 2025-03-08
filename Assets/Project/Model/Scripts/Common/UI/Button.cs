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

    public int Value { get; set; }

    internal UpDownButtonModel()
    {
        Value = 0;
    }

    internal void Destroy()
    {
        _destroyer.OnNext(true);
    }

    ~UpDownButtonModel()
    {
        _destroyer.OnNext(true);
    }
}