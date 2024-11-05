using UnityEngine;

[ExecuteAlways]
public class CameraAspect : MonoBehaviour
{
    [SerializeField] private Camera _targetCamera;
    [SerializeField] Vector2 _aspect;


    private void FixedUpdate()
    {
        float screenAspect = Screen.width / (float)Screen.height; //画面のアスペクト比
        float targetAspect = _aspect.x / _aspect.y;
        float rate = targetAspect / screenAspect;

        Rect viewRect = new Rect(0, 0, 1, 1);
        if (rate < 1)
        {
            viewRect.width = rate;
            viewRect.x = 0.5f - viewRect.width * 0.5f;
        } 
        else
        {
            viewRect.height = 1 / rate;
            viewRect.y = 0.5f - viewRect.height * 0.5f;
        }
        _targetCamera.rect = viewRect;
    }
}
