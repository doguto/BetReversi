using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class PhotonGate : MonoBehaviourPunCallbacks
{
    [SerializeField] ReversiView _reversiView;
    readonly int _maxPlayerAmount = 2;

    bool _isStarted = false;

    private void StartGame()
    {
        if (_isStarted) return;

        _isStarted = true;
        //PhotonNetwork.Instantiate("test", Vector3.zero, Quaternion.identity);
        _reversiView.InitializeReversi(false);
    }

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
        StartGame();
        Debug.Log("my player");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != _maxPlayerAmount) return;
        PhotonNetwork.Instantiate("test", Vector3.zero, Quaternion.identity);
        StartGame();
        Debug.Log("other player");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = _maxPlayerAmount }, TypedLobby.Default);
        Debug.Log(message);
    }

}
