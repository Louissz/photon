using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    string gameVersion = "1";
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogErrorFormat(gameObject,
                    "Multiple instances of {0} is not allow", GetType().Name);
            DestroyImmediate(gameObject);
            return;
        }
        PhotonNetwork.AutomaticallySyncScene = true;
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.GameVersion = gameVersion;
        SceneManager.sceneLoaded += OnSceneLoaded;

    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!PhotonNetwork.InRoom)

        {
            return;
        }
        localPlayer = PhotonNetwork.Instantiate("TankPlayer", new Vector3(0, 0, 0), Quaternion.identity, 0);
        Debug.Log("Player Instance ID: " + localPlayer.GetInstanceID());
    }


    public override void OnConnected()
    {
        Debug.Log("PUN Connected");
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("PUN Connected to Master");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Disconnected was called by PUN with reason {0}", cause);
    }

    public void JoinGameRoom()
    {
        var options = new RoomOptions
        {
            MaxPlayers = 6
        };
        PhotonNetwork.JoinOrCreateRoom("Fuck up", options, null);
    }


    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Created room!!");
            PhotonNetwork.LoadLevel("GameScene");
        }
        else
        {
            Debug.Log("Joined room!!");
        }
    }


    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Join GameRoom Failed Message : {message}");
    }


    public static GameObject localPlayer;


}