using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    float speed = 5;
    public int jumpSpeed;
    Vector2 velocity;

    PhotonView view;
    bool isTurn;
    public bool optionsOn;

    GameObject options;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        view = GetComponent<PhotonView>();
        options = GameObject.FindWithTag("options");
        optionsOn = false;

        if (!optionsOn)
        {
            options.transform.GetChild(0).GetComponent<Image>().color = new Color(options.GetComponentInChildren<Image>().color.r, options.GetComponentInChildren<Image>().color.g, options.GetComponentInChildren<Image>().color.b, 0);

            
            options.transform.GetChild(1).GetComponent<Button>().interactable = false;
            options.transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.zero;

        }

    }

    // Update is called once per frame
    void Update()
    {
        isTurn = FindObjectOfType<TurnRotation>().enable;

       
          if(isTurn)
            move();

    }


    void move()
    {
        if (view.IsMine)
        {



            if (Input.GetKey(KeyCode.A) && !optionsOn)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.D) && !optionsOn)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !optionsOn)
            {

                options.transform.GetChild(0).GetComponent<Image>().color = new Color(options.GetComponentInChildren<Image>().color.r, options.GetComponentInChildren<Image>().color.g, options.GetComponentInChildren<Image>().color.b, 0.7f);

                options.transform.GetChild(1).GetComponent<Button>().interactable = true;
                options.transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.one;
               
                optionsOn = true;
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && optionsOn)
            {

                options.transform.GetChild(0).GetComponent<Image>().color = new Color(options.GetComponentInChildren<Image>().color.r, options.GetComponentInChildren<Image>().color.g, options.GetComponentInChildren<Image>().color.b, 0);
                
                options.transform.GetChild(1).GetComponent<Button>().interactable = false;
                options.transform.GetChild(1).GetComponent<RectTransform>().localScale = Vector3.zero;
                
                optionsOn = false;
            }



            }
    }


    

    private void FixedUpdate()
    {
        if (view.IsMine && isTurn && !optionsOn)
        {
            if (onLand())
            {

                if (Input.GetKey(KeyCode.Space))
                {
                    rb.velocity += new Vector2(0, jumpSpeed * Time.fixedDeltaTime);
                }
            }
        }   
    }

    bool onLand()
    {

        int layermask = LayerMask.GetMask("Land");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, layermask);

        
           
        if (hit.collider != null)
        {
            Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.green);
            return true;
        }
        else
        {
            Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.red);
            Debug.DrawRay(transform.position, Vector2.down * 1.5f, Color.red);
            return false;
        }
                
       
        

    }

    


}
