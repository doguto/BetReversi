using UnityEngine;
using UniRx;
using Project.Reversi.Model;

namespace Project.Reversi.Presenter
{
    public class OthelloPresenter
    {
        public readonly Vector2Int Position;
        public readonly int Amount = 1;
        public readonly bool ByPlayer = true;
        public ReactiveProperty<OthelloColor> Color { get; private set; } = new();


        public OthelloPresenter(Vector2Int position, OthelloColor color, bool byPlayer = true)
        {
            Position = position;
            Color.Value = color;
            ByPlayer = byPlayer;
        }

        internal void ChangeColor()
        {
            if (Color.Value == OthelloColor.black)
            {
                Color.Value = OthelloColor.white;
            } 
            else
            {
                Color.Value = OthelloColor.black;
            }
        }
    }

}