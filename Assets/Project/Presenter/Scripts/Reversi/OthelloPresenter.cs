using UnityEngine;
using UniRx;

public class OthelloPresenter
{
    public readonly Vector2Int position;
    readonly float _othelloRotateTime = 0.5f;

    public ReactiveProperty<OthelloColor> color { get; private set; } = new ReactiveProperty<OthelloColor>();


    public OthelloPresenter(Vector2Int position, OthelloColor color)
    {
        this.position = position;
        this.color.Value = color;
    }

    internal void ChangeColor()
    {
        if (color.Value == OthelloColor.black)
        {
            color.Value = OthelloColor.white;
        } 
        else
        {
            color.Value = OthelloColor.black;
        }
    }
}
