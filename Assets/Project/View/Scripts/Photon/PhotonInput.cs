using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Project.Reversi.Model;
using Project.Reversi.Presenter;
using UnityEngine.Serialization;

namespace Project.Photon
{
    public class PhotonInput : MonoBehaviourPunCallbacks
    {
        ReversiPresenter _reversiPresenter;
        [SerializeField] PhotonView view;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var input = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var pos = new Vector2Int((int)Mathf.Round(input.x), (int)Mathf.Round(input.y));
                view.RPC(nameof(GetOthelloSetOperation), RpcTarget.Others, pos);
            }
        }


        // OthelloSet���󂯎��֐�
        [PunRPC]
        internal void GetOthelloSetOperation(Vector2 position)
        {
            if (position.x < 0 || 8 < position.x) return;
            if (position.y < 0 || 8 < position.y) return;

            Debug.Log("get other player's set operation");
            Vector2Int pos = new Vector2Int((int)position.x, (int)position.y);
            Vector2Int switchedInput = SwitchCoodinate(pos);
            ReversiModel.SetOpponentOthello(switchedInput);
        }

        // EndReversi()���󂯎��֐�
        [PunRPC]
        internal void EndReversi()
        {
        }

        // ShowResult()���󂯎��֐�
        [PunRPC]
        internal void ShowResult()
        {
        }


        Vector2Int SwitchCoodinate(Vector2Int pos)
        {
            if (pos.x > 8 || pos.x < 0) return new Vector2Int(-1, -1);
            if (pos.y > 8 || pos.y < 0) return new Vector2Int(-1, -1);

            var switched = new Vector2Int(ReversiModel.Length - 1 - pos.x, ReversiModel.Length - 1 - pos.y);
            return switched;
        }
    }
}