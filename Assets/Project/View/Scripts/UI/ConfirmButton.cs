using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ConfirmButton : ButtonBase
{
    public ReactiveProperty<bool> IsClicked { get; protected set; } = new ReactiveProperty<bool>();

    protected override void OnMouseDown()
    {
        IsClicked.Value = true;
    }
}
