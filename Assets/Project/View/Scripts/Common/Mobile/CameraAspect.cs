using Project.Main;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Common.View
{
    [ExecuteAlways]
    public class CameraAspect : MonoBehaviour
    {
        [SerializeField] Camera targetCamera;

        // [SerializeField] Vector2 _aspect;
        readonly Vector2 origin = new(0.5f, 0); // 画面の中心


        private void Awake()
        {
            var currentAspect = Screen.width / (float)Screen.height;
            var targetAspect = Main.Main.Aspect.x / Main.Main.Aspect.y;
            var targetRate = targetAspect / currentAspect;
            var viewRect = new Rect(0, 0, 1, 1);

            if (targetRate < 1)
            {
                viewRect.width = targetRate;
                viewRect.x = origin.x - viewRect.width * 0.5f;
            }
            else
            {
                viewRect.height = 1 / targetRate;
                viewRect.y = origin.x - viewRect.height * 0.5f;
            }

            targetCamera.rect = viewRect;
        }
    }
}