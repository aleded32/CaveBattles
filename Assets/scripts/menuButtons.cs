using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class menuButtons : MonoBehaviourPunCallbacks
{

    GameObject player;

    public void onPlayButton()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void onOptionButton()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void onExitButton()
    {
        Application.Quit();
    }

    public void onBackButton()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }


   
    public void onLeaveButton()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public override void OnLeftRoom()
    {
        Destroy(FindObjectOfType<seedGen>().gameObject);
    }

    public void onLeaveEndScreen()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
