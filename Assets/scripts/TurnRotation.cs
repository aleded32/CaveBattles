using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TurnRotation : MonoBehaviourPunCallbacks
{

    public int currentTurn = 0;

    Player[] playerList;
    public bool enable;
    public Text currentTurnText;
   

    private void Start()
    {
       
        playerList = PhotonNetwork.PlayerList;
        enable = false;
       
       
        

    }

    private void Update()
    {
        checkRoomNumbers();
        currentTurnText.text = "Current Turn: " + playerList[currentTurn].NickName;

        

        if (checkTurn())
        {
            enable = true;
        }
        else if(!checkTurn())
        {
            enable = false;
        }
    }

    void checkRoomNumbers()
    {
        wonGame();
    }

    bool checkTurn()
    {
        if (playerList[currentTurn].NickName == PhotonNetwork.LocalPlayer.NickName)
            return true;
        else if(playerList[currentTurn].NickName != PhotonNetwork.LocalPlayer.NickName)
            return false;
        else
            return false;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

        playerList = PhotonNetwork.PlayerList;
        if (currentTurn >= playerList.Length -1)
        {
            currentTurn = 0;
        }
        else
        {
            currentTurn++;
        }
    }

    void wonGame()
    {
        if (playerList.Length <= 1)
        {
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(5, LoadSceneMode.Single);
        }
    }
    
}
