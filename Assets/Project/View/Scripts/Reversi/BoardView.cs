using UnityEngine;
using UniRx;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(Collider2D))]
public class BoardView : MonoBehaviour, IClicked
{
    BoardPresenter _presenter;

    private void Awake()
    {
        _presenter = new BoardPresenter();
    }

    public void OnClicked(Vector3 position)
    {
        Vector2Int grid = new Vector2Int((int)Mathf.Round(position.x), (int)Mathf.Round(position.y));
        _presenter.MouseInput.OnNext(grid);
    }
}
