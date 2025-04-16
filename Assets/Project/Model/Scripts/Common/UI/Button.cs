using UniRx;
using System;

namespace Project.Common.Model
{
    public class ButtonModelBase  // 要修正。Genericの必要無し
    {
        // Subject<T> subject = new Subject<T>();
        // public IObservable<T> Subject => subject;
    }


    public class CheckButtonModel : ButtonModelBase
    {
        public bool isChecked;

        internal CheckButtonModel()
        {
            isChecked = false; 
        }
    }


    public class UpDownButtonModel : ButtonModelBase
    {
        readonly Subject<bool> destroyer = new();
        public IObservable<bool> Destroyer => destroyer;

        public int Value { get; set; }

        internal UpDownButtonModel()
        {
            Value = 0;
        }

        internal void Destroy()
        {
            destroyer.OnNext(true);
        }

        ~UpDownButtonModel()
        {
            destroyer.OnNext(true);
        }
    }
}