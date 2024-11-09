using UnityEngine;
using UniRx;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(Collider2D))]
public class BoardView : MonoBehaviour //, IClicked
{
    BoardPresenter _presenter;

    internal void InitializeBoard()
    {
        _presenter = new BoardPresenter();
    }

    private void OnMouseDown()
    {
        if (_presenter == null) return;

        Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int grid = new Vector2Int((int)Mathf.Round(position.x), (int)Mathf.Round(position.y));
        _presenter.MouseInput.OnNext(grid);
    }

    //public void OnClicked(Vector3 position)
    //{
    //    Vector2Int grid = new Vector2Int((int)Mathf.Round(position.x), (int)Mathf.Round(position.y));
    //    _presenter.MouseInput.OnNext(grid);
    //}
}
