using UnityEngine;

public class CheckButton : ButtonViewBase
{
    // public ReactiveProperty<bool> IsClicked { get; protected set; } = new ReactiveProperty<bool>(false);
    private CheckButtonPresenter _presenter;
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
            _presenter.IsChecked = value;
            if (value) Destroy(gameObject);
        } 
    }

    internal void Init(CheckButtonPresenter presenter)
    {
        _presenter = presenter;
    }

    protected override void OnMouseDown()
    {
        Debug.Log("ConfirmButton.OnMouseDown()");
        IsChecked = true;
    }
}
