using UnityEngine;
using UnityEngine.Networking;
[NetworkSettings(channel = 1)]
public class Disparo : NetworkBehaviour {
    //Animaciones
    public bool SacarGuardar;
    //Disparo
    public GameObject Bala;
    public Transform SpawnBala;
    public int VelocidadBala;
    //Raycast
    public RaycastHit hit;
    public Camera cam;
    private GameObject play;

    // Use this for initialization
    void Start () {
      
    }

    // Update is called once per frame
    [ClientCallback]
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        SacarGuardar = Input.GetButton("Fire2") ? true : SacarGuardar;
        if (SacarGuardar == true)
            if (Input.GetButtonDown("Fire1"))
            {
                CmdDisparos();
                CmdInstanciarBalaServer();
            }
    }

    [Command]
    void CmdDisparos()
    {
        if (Physics.Raycast(SpawnBala.transform.position, SpawnBala.transform.forward, out hit, 100) && hit.collider.gameObject.tag == "Player")
        {
            //Debug.DrawLine(SpawnBala.transform.position,hit.point);
            //Debug.Log(hit.point);
            play = Diccionario.GetPlayer(hit.transform.name);
            Vida health = play.GetComponent<Vida>();
            if (health != null)
            {
                health.TakeDamage(10);
            }
        }
    }
    [Command]
    void CmdInstanciarBalaServer()
    {
        RpcInstanciarBalaClient();
    }
    [ClientRpc]
    void RpcInstanciarBalaClient()
    {
        GameObject instanciaBala = (GameObject)Instantiate(Bala,SpawnBala.transform.position,Quaternion.identity);
        instanciaBala.GetComponent<Rigidbody>().velocity =SpawnBala.forward*VelocidadBala;
    }


}
