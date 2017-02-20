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
        playerId = GetComponent<NetworkIdentity>().netId;
        string netId = playerId.ToString();
        Diccionario.RegisterPlayer(netId, this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
