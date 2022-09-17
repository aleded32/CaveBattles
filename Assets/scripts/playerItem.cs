using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerItem : MonoBehaviour
{
    public void displayName(string _playerName)
    {
        GetComponentInChildren<Text>().text = _playerName;
    }
    
}
