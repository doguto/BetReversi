using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Threading.Tasks;
using System.Threading;


public class PhotonGate : MonoBehaviourPunCallbacks
{
    [SerializeField] ReversiView _reversiView;
    readonly int _maxPlayerAmount = 2;

    bool _isStarted = false;


    private void Start()
    {
        Debug.Log("Try to connect PUN2.");
        PhotonNetwork.ConnectUsingSettings();
    }


    private void StartGame(bool isSoloGame, bool isFirstIn)
    {
        if (_isStarted) return;

        _isStarted = true;
        PhotonNetwork.Instantiate("photon", Vector3.zero, Quaternion.identity);
        Debug.Log("Starting Game");
        _reversiView.InitializeReversi(isSoloGame, isFirstIn);
    }

    private async Task StartSoloGame()
    {
        await Task.Run(() =>
        {
            Thread.Sleep(15000);  // wait 15 seconds
            if (_isStarted) return;
            Debug.Log("Start game in solo mode");
            //StartGame(true);
        });
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master room");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined to Gaming room.");
        Debug.Log($"PlayerCount: {PhotonNetwork.CurrentRoom.PlayerCount}");
        Debug.Log($"MaxPlayerAmount: {PhotonNetwork.CurrentRoom.MaxPlayers}");

        if (PhotonNetwork.CurrentRoom.PlayerCount != _maxPlayerAmount)
        {
            // var soloGame = Task.Run(() => StartSoloGame());
            return;
        }

        StartGame(false, false);
        Debug.Log("my player");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount != _maxPlayerAmount) return;
        StartGame(false, true);
        Debug.Log("other player");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = _maxPlayerAmount }, TypedLobby.Default);
        Debug.Log(message);
    }
}
