using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;



public class weaponBlast : MonoBehaviour
{ 
    Vector2 dis = new Vector2(0,0);
    float angle = 0;
    public GameObject bullet;
    float maxSpeed;
    public float speed;

    GameObject instBullet;
    bool isShooting, isShot;

    PhotonView view;

    Vector2 spawnBulletPos;

    bool isTurn;

    public GameObject powerBar;
   
    


    void Start()
    {
        view = GetComponent<PhotonView>();
       speed = 0;
       maxSpeed = 800;
        isShot = false;
        //isNotTurn = FindObjectOfType<TurnRotation>().notCurrentTurn;
        spawnBulletPos = new Vector2(0, 0);
        
        

    }


    // Update is called once per frame
    void Update()
    {
        isTurn = FindObjectOfType<TurnRotation>().enable;

        if (isTurn && !GetComponent<playerMovement>().optionsOn)
            shoot();


        updatePowerBar();
        
        
        
    }

    private void shoot()
    {
        dis = new Vector2(transform.position.x - mousePos().x, transform.position.y - mousePos().y);
        angle = Mathf.Atan2(-dis.y, -dis.x);
       

        if (view.IsMine)
        {

            if (Input.GetMouseButtonDown(0))
            {
                isShooting = true;

            }
            else if (Input.GetMouseButtonUp(0))
            {
                
                PhotonNetwork.Instantiate(bullet.name, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.identity);
                GameObject.FindGameObjectWithTag("bullet").GetComponent<Rigidbody2D>().velocity += new Vector2(Mathf.Cos(angle) * speed * Time.deltaTime, Mathf.Sin(angle) * speed * Time.deltaTime);
                isShooting = false;
                
               

            }


            if (GameObject.FindGameObjectWithTag("bullet"))
            {
                
                Vector2 screenPos = Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("bullet").transform.position);
                if (screenPos.x < 0 || screenPos.x > Camera.main.scaledPixelWidth || screenPos.y < 0)
                {
                    speed = 0;
                    PhotonNetwork.Destroy(GameObject.FindGameObjectWithTag("bullet"));

                }

            }

            if (isShooting && speed <= maxSpeed)
            {
                speed += 150 * Time.deltaTime;
            }
            else if (speed > maxSpeed)
            {
                speed = 0;
            }

            


        }

       
    }

   

    Vector3 mousePos()
    {
       Vector3 pixelPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (pixelPos.x < 0 || pixelPos.y < 0)
        {
            return new Vector2(0,0);
        }
        else
        {
            return new Vector2(Mathf.RoundToInt(pixelPos.x), Mathf.RoundToInt(pixelPos.y));
        }

       
    }

    void updatePowerBar()
    {
        powerBar.transform.localScale = new Vector3(speed * 0.00025f, 0.04f, 1);
    }

}
