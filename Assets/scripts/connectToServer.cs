using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;

public class connectToServer : MonoBehaviourPunCallbacks
{
    public InputField Itext;
    public Text btnText;

    public void onConnectButton()
    {
        if (Itext.text.Length > 1)
        {
            PhotonNetwork.NickName = Itext.text;
            btnText.text = "Connecting...";
            PhotonNetwork.ConnectUsingSettings();
            PhotonNetwork.AutomaticallySyncScene = true;
        }
    }


    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("lobbyScene");
    }
}
