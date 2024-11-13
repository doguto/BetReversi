using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PhotonManager : MonoBehaviourPunCallbacks
{
    [SerializeField] ReversiView _reversiView;
    readonly int _maxPlayerAmount = 2;


    private void Start()
    {
        Debug.Log("Try to connect PUN2.");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master room");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined to Gaming room.");
        Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        Debug.Log(PhotonNetwork.CurrentRoom.MaxPlayers);

        if (PhotonNetwork.CurrentRoom.PlayerCount != _maxPlayerAmount) return;
        PhotonNetwork.Instantiate("test", Vector3.zero, Quaternion.identity);
        _reversiView.InitializeReversi(false);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != _maxPlayerAmount) return;
        PhotonNetwork.Instantiate("test", Vector3.zero, Quaternion.identity);
        _reversiView.InitializeReversi(false);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = _maxPlayerAmount }, TypedLobby.Default);
        Debug.Log(message);
    }

}
