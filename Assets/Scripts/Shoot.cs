using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

[NetworkSettings(channel = 1)]
public class Shoot : NetworkBehaviour {

    //Animations
    public bool TakeWeapon = true;

    //Shoot variables
    public GameObject Bullet;
    public float SpeedBullet;
    public Transform SpawnBullet;

    private RaycastHit hit;
    private GameObject objectivePlayer;

    //Charge the library of weapons
    private ArsenalWeapons arsenal;
    public GameObject[] Weapons;

    //Prueba
    public int damage = 10;

    // Use this for initialization
    void Start () {
        arsenal = new ArsenalWeapons();
        foreach (KeyValuePair<int,PropertiesWeapon> x in arsenal.Weapons)
        {
            GameObject identifierClass = GetComponent<Identifier>().IdentifierClass;
            if (x.Value.NameClass == identifierClass.name && x.Value.IsUsing)
            {
                Bullet = Weapons[x.Key];
                SpeedBullet = x.Value.SpeedBullet;
            }
        }
    }

    // Update is called once per frame
    [ClientCallback]
    void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        if (TakeWeapon == true)
            shootFire(GetComponent<State>().Stunned);
    }

    public void shootFire(bool stunned)
    {
        if(!stunned)
        if (Input.GetButtonDown("Fire1"))
        {
                CmdBulletServer();
                //CmdBulletServer();
        }
    }

    [Command]
    void CmdBulletServer()
    {
        if (Physics.Raycast(SpawnBullet.transform.position, SpawnBullet.transform.forward, out hit, 100) && hit.collider.gameObject.tag == "Player")
        {
            objectivePlayer = RegisterPlayers.GetPlayer(hit.transform.name);
            Health health = objectivePlayer.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }
        RpcBulletClient();
    }
    /*
    [Command]
    void CmdBulletServer()
    {
        RpcBulletClient();
    }
    */
    [ClientRpc]
    void RpcBulletClient()
    {
        GameObject instanceBullet = (GameObject)Instantiate(Bullet, SpawnBullet.transform.position,Quaternion.identity);
        instanceBullet.GetComponent<Rigidbody>().velocity = SpawnBullet.forward*SpeedBullet;
        Destroy(instanceBullet, 1.0f);
    }
}
