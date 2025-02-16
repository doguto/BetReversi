using UnityEngine;
using UniRx;

public class OthelloPresenter
{
    readonly float OthelloRotateTime = 0.5f;

    public readonly Vector2Int Position;
    public readonly bool ByPlayer = true;
    public ReactiveProperty<OthelloColor> color { get; private set; } = new ReactiveProperty<OthelloColor>();


    public OthelloPresenter(Vector2Int position, OthelloColor color, bool byPlayer = true)
    {
        this.Position = position;
        this.color.Value = color;
        this.ByPlayer = byPlayer;
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
