using UnityEngine;
using System.Collections.Generic;


public class RegisterPlayers : MonoBehaviour
{

    public static Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    public static void RegisterPlayer(string netID, GameObject Player)
    {
        players.Add(netID, Player);
        Player.transform.name = netID;
    }

    public static GameObject GetPlayer(string ID)
    {
        return players[ID];
    }


}