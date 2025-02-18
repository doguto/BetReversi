using UnityEngine;

[ExecuteAlways]
public class CameraAspect : MonoBehaviour
{
    [SerializeField] private Camera _targetCamera;
    // [SerializeField] Vector2 _aspect;
    readonly Vector2 Origin = new Vector2(0.5f, 0);  // 画面の中心


    private void Awake()
    {
        float currentAspect = Screen.width / (float)Screen.height;
        float targetAspect = Main.Aspect.x / Main.Aspect.y;
        float targetRate = targetAspect / currentAspect;
        Rect viewRect = new Rect(0, 0, 1, 1);

        if (targetRate < 1)
        {
            viewRect.width = targetRate;
            viewRect.x = Origin.x - viewRect.width * 0.5f;
        } 
        else
        {
            viewRect.height = 1 / targetRate;
            viewRect.y = Origin.x - viewRect.height * 0.5f;
        }
        _targetCamera.rect = viewRect;
    }
}
