using UnityEngine;

public class MousePort : MonoBehaviour
{
    Transform _transform;
    float _firstZ;
    bool _isClicking = false;

    private void Awake()
    {
        _transform = transform;
        _firstZ = transform.position.z;
    }

    private void Update()
    {
        OnLeftClicked();
    }

    void OnLeftClicked()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = _firstZ;
        this._transform.position = pos;
        _isClicking = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!_isClicking) return;

        IClicked clicked = collision.gameObject.GetComponent<IClicked>();
        if (clicked == null) return;

        clicked.OnClicked(_transform.position);
        _isClicking = false;
    }
}

