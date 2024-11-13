using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class testRPC : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject _hoge;

    //int _hoge = 0;
    float _last = 0;
    private void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        photonView.RPC(nameof(WriteDebug), RpcTarget.All, pos);
    }

    [PunRPC]
    void WriteDebug(Vector3 pos)
    {
        //if (Time.time - _last < 1.0f) return;
        if (!Input.GetKeyUp(KeyCode.Space)) return;

        Debug.Log("hoge");
        Instantiate(_hoge, pos, Quaternion.identity);
        //Debug.Log();
        //_hoge++;
    }
}
