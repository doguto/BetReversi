using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Serialization;

[RequireComponent(typeof(PhotonView))]
public class testRPC : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject hoge;
    [SerializeField] PhotonView photonView;

    private void Update()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            photonView.RPC(nameof(WriteDebug), RpcTarget.All, pos);
        }
    }

    [PunRPC]
    void WriteDebug(Vector3 pos, PhotonMessageInfo info)
    {
        pos.z = 0;
        Debug.Log("hoge from :" + info.Sender);
        Instantiate(hoge, pos, Quaternion.identity);
    }
}