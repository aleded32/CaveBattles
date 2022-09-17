using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roomItem : MonoBehaviour
{
    public Text roomName;
    public lobbyManager lm;

    private void Start()
    {
        lm = FindObjectOfType<lobbyManager>();
    }

    public void setRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void onClickItem()
    {
        lm.joinRoom(roomName.text);
    }
    
}
