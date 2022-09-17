using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class lobbyManager : MonoBehaviourPunCallbacks
{

    public InputField Iroom;
    public GameObject lobbyPanel;
    public GameObject roomPanel;
    public Text roomName;

    public roomItem roomItemPrefab;
    List<roomItem> roomitemList = new List<roomItem>();
    public Transform contentObject;

    float UpdateTime = 1.5f;
    float nextUpdateTime;

    public List<playerItem> playerItemList = new List<playerItem>();
    public playerItem playerItemPrefab;
    public Transform playerItemTransform;

    public GameObject playBtn;

    public void Start()
    {
        PhotonNetwork.JoinLobby();
    }

    public void onCreateBtn()
    {
        if (Iroom.text.Length > 1)
        {
            PhotonNetwork.CreateRoom(Iroom.text, new RoomOptions() { MaxPlayers = 4 });
        }
    }

    public void onBackBtn()
    {
        Destroy(FindObjectOfType<seedGen>().gameObject);
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);

        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;

        updatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> _roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(_roomList);
            nextUpdateTime = Time.time + UpdateTime;
        }

        
    }

    private void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (roomItem r in roomitemList)
        {
            Destroy(r.gameObject);
        }
        roomitemList.Clear();

        foreach (RoomInfo room in list)
        {
            roomItem newRoom = Instantiate(roomItemPrefab, contentObject);
            newRoom.setRoomName(room.Name);
            roomitemList.Add(newRoom);
        }

    }

    public void joinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void onClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        
        roomPanel.SetActive(false);
        lobbyPanel.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    void updatePlayerList()
    {
        foreach (playerItem p in playerItemList)
        {
            Destroy(p.gameObject);
        }

        playerItemList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> p in PhotonNetwork.CurrentRoom.Players)
        {
            playerItem newP = Instantiate(playerItemPrefab, playerItemTransform);
            newP.displayName(p.Value.NickName);
            playerItemList.Add(newP);

        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        updatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        updatePlayerList();
    }

    public void Update()
    {
       

        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playBtn.SetActive(true);
        }
        else
        {
            playBtn.SetActive(false);
        }
    }

    public void onClickPlayBtn()
    {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.LoadLevel("gameScene");   
    }
}
