using UnityEngine;
using Project.Common.Model;

namespace Project.Reversi.Model
{
    public class SetOthelloMessage
    {
        public Vector2Int Position { get; private set; }
        public OthelloColor Color { get; private set; }
        public bool ByPlayer { get; private set; }
        public CheckButtonModel ConfirmButtonModel { get; private set; }
        public UpDownButtonModel UpDownButtonModel { get; private set; }

        internal SetOthelloMessage(Vector2Int position, OthelloColor color, bool byPlayer = false)
        {
            Position = position;
            Color = color;
            ByPlayer = byPlayer;
        }

        internal SetOthelloMessage(Vector2Int position, OthelloColor color, CheckButtonModel confirmButtonModel, UpDownButtonModel upDownButtonModel)
        {
            Position = position;
            Color = color;
            ByPlayer = true;
            ConfirmButtonModel = confirmButtonModel;
            UpDownButtonModel = upDownButtonModel;
        }
    }
}