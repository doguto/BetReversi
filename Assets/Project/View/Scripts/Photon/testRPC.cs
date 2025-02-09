using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[RequireComponent (typeof(PhotonView))]
public class testRPC : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _hoge;
    [SerializeField] PhotonView _photonView;

    //int _hoge = 0;
    float _last = 0;
    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            _photonView.RPC(nameof(WriteDebug), RpcTarget.All, pos);
        }
    }

    [PunRPC]
    void WriteDebug(Vector3 pos, PhotonMessageInfo info)
    {
        pos.z = 0;
        Debug.Log("hoge from :" + info.Sender);
        Instantiate(_hoge, pos, Quaternion.identity);
    }
}
