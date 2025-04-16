using UnityEngine;

namespace Project.Common.View
{
    public class GameScreen : Singleton<GameScreen>
    {
        static Camera _mainCamera;

        void Start()
        {
            _mainCamera = Camera.main;
        }

        // Screen範囲内の座標を返す。双方Screen範囲内の場合はpositionを返す。
        internal static Vector3 GetValidPosition(Vector3 position, Vector3 spare)
        {
            if (IsInScreen(position)) return position;
            if (IsInScreen(spare)) return spare;

            Debug.LogError("invalid Positions");
            return Vector3.zero;
        }

        // Screenの端ギリギリの場合は範囲外とみなす。
        static bool IsInScreen(Vector3 position)
        {
            var screenPosition = _mainCamera.WorldToScreenPoint(position); // 左下を原点とし、画素数を座標としたもの

            var screenLeftButtom = new Vector3(Screen.width * _mainCamera.rect.xMin,
                Screen.height * _mainCamera.rect.yMin, 0);
            var screenRightTop = new Vector3(Screen.width * _mainCamera.rect.xMax,
                Screen.height * _mainCamera.rect.yMax, 0);

            // 画面の端ギリギリはタッチしにくいので、少し内側にする。
            var screenRange = screenRightTop - screenLeftButtom;
            screenLeftButtom += screenRange * 0.1f;
            screenRightTop -= screenRange * 0.1f;

            var xIn = screenLeftButtom.x < screenPosition.x && screenPosition.x < screenRightTop.x;
            var yIn = screenLeftButtom.y < screenPosition.y && screenPosition.y < screenRightTop.y;
            return xIn && yIn;
        }
    }
}