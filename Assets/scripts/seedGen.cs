using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class seedGen : MonoBehaviourPunCallbacks, IPunObservable
{ 
    public int seed;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(seed);
        }
        else if (stream.IsReading)
        {
            seed = (int)stream.ReceiveNext();
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {

        if(PhotonNetwork.IsMasterClient == true  && seed == 0)
            seed = Random.Range(-10000, 10000);
        

       
    }

    

    

}
