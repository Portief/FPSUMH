using UnityEngine;
using System.Collections.Generic;


public class Diccionario : MonoBehaviour
{

    public static Dictionary<string, GameObject> player = new Dictionary<string, GameObject>();

    public static void RegisterPlayer(string netID, GameObject Player)
    {
        player.Add(netID, Player);
        Player.transform.name = netID;
    }

    public static GameObject GetPlayer(string ID)
    {
        return player[ID];
    }


}