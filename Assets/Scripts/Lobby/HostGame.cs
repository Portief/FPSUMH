using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class HostGame : MonoBehaviour
{
    [SerializeField]
    private uint roomSize = 10;

    private string roomName;

    public NetworkManager networkManager;

    void Start()
    {
        
        if (networkManager.matchMaker == null)
            networkManager.StartMatchMaker();
    }

    public void setRoomName (string name)
    {
        roomName = name;
    }

    public void CreateRoom ()
    {
        if(roomName != "" && roomName != null)
        {
            networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
        }
    }

}
