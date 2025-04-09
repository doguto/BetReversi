using UnityEngine;

namespace Project.Reversi.Model
{
    public class ChangeColorMessage
    {
        public Vector2Int Position {  get; private set; }
        public ChangeColorMessage(Vector2Int position)
        {
            Position = position;
        }
    }
}