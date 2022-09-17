using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class mapGen : MonoBehaviour
{

    public int width;
    public int height;
    float scale = 20f;

    int seed;

    [Range(0,1)] public float modifier = 0.2f;
    int maxY;

    public GameObject landTile;
    public List<GameObject> tileList;

    // Start is called before the first frame update
    void Start()
    {
        maxY = height - 2;
        tileList = new List<GameObject>();
        if (!FindObjectOfType<seedGen>())
            seed = Random.Range(-10000, 10000);
        else
            seed = FindObjectOfType<seedGen>().seed;


        //seed = Random.Range(-10000, 10000);
        MapGen();
       
    }

    

    void MapGen()
    {

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++) 
            {
               
              
                    int perlin = Mathf.RoundToInt(Mathf.PerlinNoise((j * modifier) + seed, (i * modifier) + seed));

                    if (perlin == 1)
                    {
                        if (PhotonNetwork.IsConnected)
                             tileList.Add(PhotonNetwork.Instantiate(landTile.name, new Vector3(j, i, 0), Quaternion.identity));
                        
                    }
                
               
            }
        }
       
    }


    
}
