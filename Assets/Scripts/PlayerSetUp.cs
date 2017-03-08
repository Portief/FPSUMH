using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSetUp : NetworkBehaviour
{

    [SerializeField]
    private NetworkInstanceId playerId;
    // Use this for initialization
    void Start()
    {
        //Change the name of the players whit the id
        playerId = GetComponent<NetworkIdentity>().netId;
        string netId = playerId.ToString();
        RegisterPlayers.RegisterPlayer(netId, this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject x in RegisterPlayers.players.Values)
        {
            //Debug.Log(x);
        }
    }
}
