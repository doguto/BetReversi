using UnityEngine;
using UniRx;
using Project.Reversi.Model;
using Project.Reversi.Presenter;

namespace Project.Reversi.View
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class BoardView : MonoBehaviour
    {
        BoardPresenter _presenter;

        internal void InitializeBoard()
        {
            _presenter = new BoardPresenter();
        }

        void OnMouseDown()
        {
            if (_presenter == null) return;
            if (Camera.main == null) return;

            var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var grid = new Vector2Int((int)Mathf.Round(position.x), (int)Mathf.Round(position.y));
            _presenter.MouseInput.OnNext(grid);
        }
    }
}