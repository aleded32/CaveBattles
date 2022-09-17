using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class spawnPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playerPrefab;
    int numberOfplayers = 4;
    List<Photon.Realtime.Player> players;

    private void Start()
    {
        players = PhotonNetwork.PlayerList.ToList();

        int index = players.FindIndex(x => x.NickName == PhotonNetwork.NickName);

        PhotonNetwork.Instantiate(playerPrefab.name, new Vector2((6+index) * (index +1), 26), Quaternion.identity);
    }
}
