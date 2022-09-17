using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class bulletCollision : MonoBehaviour
{

    PhotonView view;
  
    mapGen mg;
    GameObject landTileToBeDes;
    playerHealth ph;
    bool isCollidedPlayer;
    int viewID;
    public int damage;
    bool turnPassed;


    private void Start()
    {
       
        view = GetComponent<PhotonView>();
        mg = FindObjectOfType<mapGen>();
        isCollidedPlayer = false;
        damage = 20;
        turnPassed = false;
        
    }


    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "land")
        {
            

           
            

            if (view && view.IsMine)
            {
               
                viewID = collision.collider.GetComponent<PhotonView>().ViewID;
                

                
                view.RPC("changeTurn", RpcTarget.AllBuffered);
                view.RPC("destroyWall", RpcTarget.AllBuffered, viewID);
                view.RPC("DestroyBullet", RpcTarget.AllBuffered, view.GetComponent<PhotonView>().ViewID);
                
            }
        }

        if (collision.collider.tag == "player")
        {


            if (view && view.IsMine)
            {

                    viewID = collision.collider.GetComponent<PhotonView>().ViewID;
                

                if (PhotonNetwork.LocalPlayer.NickName != collision.collider.GetComponent<PhotonView>().Owner.NickName)
                        view.RPC("takeDamage", RpcTarget.AllBuffered, viewID);


                    view.RPC("changeTurn", RpcTarget.AllBuffered);
                    view.RPC("DestroyBullet", RpcTarget.AllBuffered, view.GetComponent<PhotonView>().ViewID);
                
                
            }

        }
    }








    [PunRPC]
    private void DestroyBullet(int viewID)
    {
        Destroy(PhotonNetwork.GetPhotonView(viewID).gameObject);
    }

    
    [PunRPC]
    void destroyWall(int viewID)
    {

        Destroy(PhotonNetwork.GetPhotonView(viewID).gameObject);

        
    }

    [PunRPC]
    private void takeDamage(int viewID)
    {
       
        PhotonNetwork.GetPhotonView(viewID).gameObject.GetComponent<playerHealth>().health -= damage;
    }

    [PunRPC]
    void changeTurn()
    {
        GameObject.FindWithTag("player").GetComponent<weaponBlast>().speed = 0;
        if (FindObjectOfType<TurnRotation>().currentTurn >= PhotonNetwork.PlayerList.Length - 1 && !turnPassed)
        {
            
            FindObjectOfType<TurnRotation>().currentTurn = 0;
            turnPassed = true;
        }
        else if (FindObjectOfType<TurnRotation>().currentTurn < PhotonNetwork.PlayerList.Length - 1 && !turnPassed)
        {
            
            FindObjectOfType<TurnRotation>().currentTurn++;
            turnPassed = true;
        }
    }
    
}
