using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class playerHealth : MonoBehaviour
{

    public int health;
    public GameObject healthBar;
    public TextMesh playerName;
    public PhotonView view;

    void Start()
    {
        health = 100;


        playerName.text = view.Owner.NickName;
        
    }

    // Update is called once per frame
    void Update()
    {
        lostGame();
        updateHealth();
    }

    void updateHealth()
    {
        healthBar.transform.localScale = new Vector3(((float)health * 0.002f), 0.04f, 1); 
    }

    void lostGame()
    {
        if (view.IsMine)
        {
            if (health <= 0)
            {
                PhotonNetwork.LeaveRoom();
                SceneManager.LoadScene(6, LoadSceneMode.Single);
            }
        }
    }

}
