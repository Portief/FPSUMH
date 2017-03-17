using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

[NetworkSettings(channel = 1)]
public class Shoot : NetworkBehaviour {

    //Objects
    public Objects ObjectWeapons;

    //Animations
    public Animator anim;
    public bool TakeWeapon = true;
    public bool RechargeWeapon = false;
    public bool ShootWeapon=false;
    public bool CadencyWeapon=true;

    //Shoot variables
    public int ConfigurationActive;
    public GameObject Bullet;
    public float SpeedBullet;
    public Transform SpawnBullet;
    public float cadency;
    public float DamageBullet;
    public int counterBullet = 0;
    public int numberBullets;
    public float timeRecharge = 0.0f;
    public string nameWeapon;
    public bool rechargeable;

    private RaycastHit hit;
    private GameObject objectivePlayer;

    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();

        anim.SetLayerWeight(1, 2f);
        anim.SetLayerWeight(2, 2f);

        ObjectWeapons = GetComponent<Objects>();
        ObjectWeapons.arsenal = new ArsenalWeapons();

        SearchAndAssignInLibraryWeapon();
    }

    // Update is called once per frame
    [ClientCallback]
    void Update () {

        if (!isLocalPlayer)
        {
            return;
        }

        //AnimShoot
        anim.SetBool(Animator.StringToHash("SacarGuardarArma"), TakeWeapon);
        anim.SetBool(Animator.StringToHash("Disparar"), ShootWeapon);

        //Shoot
        if (TakeWeapon == true)
        {
            shootFire(GetComponent<State>().Silenced);
        }
    }

    public void shootFire(bool silenced)
    {
        if (!silenced)
        {
            if (charger())
            {
                if (Input.GetKeyDown(KeyCode.R) && rechargeable == true)
                {
                    counterBullet = numberBullets;
                    charger();
                }
                else if (Input.GetButtonDown("Fire1"))
                {
                    if (cadencyWeapon())
                    {
                        anim.SetFloat("SpeedDispRetroceso", 1 / cadency);
                        CmdBulletServer();
                        counterBullet++;
                    }
                }
            }
        }
    }
    private bool cadencyWeapon()
    {
        if (CadencyWeapon == true)
        {
            StartCoroutine(cadencyTime(cadency));
            return true;
        }
        else
        {
            return false;
        }
    }
    private IEnumerator cadencyTime(float time)
    {
        ShootWeapon = true;
        CadencyWeapon = false;
        yield return new WaitForSeconds(2*time);
        ShootWeapon = false;
        yield return new WaitForSeconds(5*time);
        CadencyWeapon = true;
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
                health.TakeDamage(DamageBullet);
            }
        }
        RpcBulletClient();
    }
    [ClientRpc]
    void RpcBulletClient()
    {
        GameObject instanceBullet = (GameObject)Instantiate(Bullet, SpawnBullet.transform.position,Quaternion.identity);
        instanceBullet.GetComponent<Rigidbody>().velocity = SpawnBullet.forward*SpeedBullet;
        Destroy(instanceBullet, 1.0f);
    }
    private bool charger()
    {
        if (counterBullet==numberBullets)
        {
            if (RechargeWeapon == false && rechargeable==true)
            {
                StartCoroutine(chargerTime(timeRecharge));
                RechargeWeapon = true;
            }
            else if(rechargeable == false)
            {
                return true;
            }

            return false;
        }
        else
        {
            return true;
        }
    }
    private IEnumerator chargerTime(float time)
    {
        yield return new WaitForSeconds(time);
        RechargeWeapon = false;
        counterBullet = 0;
    }
    public void SearchAndAssignInLibraryWeapon()
    {
        foreach (KeyValuePair<int, PropertiesWeapon> x in ObjectWeapons.arsenal.Weapons)
        {
            GameObject identifierClass = GetComponent<Identifier>().IdentifierClass;

            if (x.Value.NameClass == identifierClass.name && x.Value.IsUsing)
            {
                ConfigurationActive = x.Key;
                Bullet = ObjectWeapons.WeaponsConfiguration[x.Key];
                cadency = x.Value.Cadency;
                numberBullets = x.Value.NumberBullets;
                SpeedBullet = x.Value.SpeedBullet;
                DamageBullet = x.Value.DamageBullet;
                timeRecharge = x.Value.TimeRecharge;
                nameWeapon = x.Value.Name;
                rechargeable = x.Value.Rechargeable;
            }
        }
    }
}
