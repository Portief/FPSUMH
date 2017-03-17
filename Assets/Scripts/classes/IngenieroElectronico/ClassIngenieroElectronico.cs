using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class ClassIngenieroElectronico : NetworkBehaviour, BaseClass
{

    //Objects
    private Objects objectWeapons;
    private Shoot myWeaponShoot;

    //Cooldowns
    public bool CooldownQ = true;
    public bool CooldownE = true;
    public bool CooldownD = true;
    public bool CooldownSI = true;
    public float TimeQ;
    public float TimeE;
    public float TimeD;
    public float TimeSI;

    //Variables Q
    private float timeStunQ = 1.0f;
    private float timeSlowQ = 1.0f;
    private float slowQ = 0.2f;
    public GameObject Grenade;
    public Transform SpawnGrenade;
    public float Angle = 30;
    public float SpeedGrenade = 15;

    //Variables Click D
    private int numberBullets;
    public int DefaultWeapon;
    public int NewWeapon;
    public bool IncreasedBullet = false;

    // Use this for initialization
    void Start()
    {
        objectWeapons = GetComponent<Objects>();
        myWeaponShoot = GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Q(GetComponent<State>().Silenced);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            E(GetComponent<State>().Silenced);
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            D(GetComponent<State>().Silenced);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SI(GetComponent<State>().Silenced);
        }
        functionRealTimeD();
    }
    void functionRealTimeD()
    {
        if (IncreasedBullet)
        {
            if (myWeaponShoot.counterBullet == numberBullets)
            {
                StartCoroutine(TimeCooldown(3, TimeD));
                foreach (KeyValuePair<int, PropertiesWeapon> x in myWeaponShoot.ObjectWeapons.arsenal.Weapons)
                {
                    if (DefaultWeapon == x.Key)
                    {
                        x.Value.IsUsing = true;
                    }
                    if (NewWeapon == x.Key)
                    {
                        x.Value.IsUsing = false;
                    }
                }
                myWeaponShoot.SearchAndAssignInLibraryWeapon();
                IncreasedBullet = false;
            }
        }
    }
    [Command]
    void CmdGrenadeServer(float angleincremental)
    {
        GameObject instanceGrenade = (GameObject)Instantiate(Grenade, SpawnGrenade.transform.position, Quaternion.identity);
        instanceGrenade.GetComponent<Rigidbody>().velocity = SpawnGrenade.transform.TransformDirection(new Vector3(0, Mathf.Sin((Angle + angleincremental) * Mathf.Deg2Rad) * SpeedGrenade, Mathf.Cos((Angle + angleincremental) * Mathf.Deg2Rad) * SpeedGrenade));
        RpcBulletClient(angleincremental);
    }
    [ClientRpc]
    void RpcBulletClient(float angleincremental)
    {

        if (!isServer)
        {
            GameObject instanceGrenade = (GameObject)Instantiate(Grenade, SpawnGrenade.transform.position, Quaternion.identity);
            instanceGrenade.GetComponent<Rigidbody>().velocity = SpawnGrenade.transform.TransformDirection(new Vector3(0, Mathf.Sin((Angle + angleincremental) * Mathf.Deg2Rad) * SpeedGrenade, Mathf.Cos((Angle + angleincremental) * Mathf.Deg2Rad) * SpeedGrenade));
        }
    }
    public void Q(bool silenced)
    {
        if (!silenced)
            if (Cooldown(1))
            {
                float angle;
                Vector3 cameraForward = objectWeapons.Cam.transform.forward;
                Vector3 cameraBase = objectWeapons.Player.transform.forward;
                Vector3 sign = cameraForward - cameraBase;
                if (sign.y >= 0)
                {
                    angle = Mathf.Acos((cameraForward.x * cameraBase.x + cameraForward.y * cameraBase.y + cameraForward.z * cameraBase.z) / (cameraForward.magnitude * cameraBase.magnitude)) / Mathf.Deg2Rad;
                }
                else
                {
                    angle = -Mathf.Acos((cameraForward.x * cameraBase.x + cameraForward.y * cameraBase.y + cameraForward.z * cameraBase.z) / (cameraForward.magnitude * cameraBase.magnitude)) / Mathf.Deg2Rad;
                }
                CmdGrenadeServer(angle);
            }
    }
    public void E(bool silenced)
    {
        if (!silenced)
            if (Cooldown(2))
            {
                Debug.Log("E");
            }
    }
    public void D(bool silenced)
    {
        if (!silenced)
            if (myWeaponShoot.RechargeWeapon == false)
            {
                if (Cooldown(3))
                {
                    DefaultWeapon = myWeaponShoot.ConfigurationActive;
                    myWeaponShoot.counterBullet = 0;
                    foreach (KeyValuePair<int, PropertiesWeapon> x in myWeaponShoot.ObjectWeapons.arsenal.Weapons)
                    {
                        if (x.Key == DefaultWeapon)
                        {
                            x.Value.IsUsing = false;
                            string nameWeapon = x.Value.Name;
                            foreach (KeyValuePair<int, PropertiesWeapon> xi in myWeaponShoot.ObjectWeapons.arsenal.Weapons)
                            {
                                if (xi.Value.Name == nameWeapon + "Increased")
                                {
                                    numberBullets = xi.Value.NumberBullets;
                                    NewWeapon = xi.Key;
                                    xi.Value.IsUsing = true;
                                    myWeaponShoot.SearchAndAssignInLibraryWeapon();
                                    IncreasedBullet = true;
                                }
                            }
                        }
                    }
                }
            }
    }
    public void SI(bool silenced)
    {
        if (!silenced)
            if (Cooldown(4))
            {

            }
    }
    public bool Cooldown(int hability)
    {
        switch (hability)
        {
            case 1:
                if (CooldownQ)
                {
                    CooldownQ = false;
                    StartCoroutine(TimeCooldown(1, TimeQ));
                    return true;
                }
                else
                {
                    return false;
                }
            case 2:
                if (CooldownE)
                {
                    CooldownE = false;
                    StartCoroutine(TimeCooldown(2, TimeE));
                    return true;
                }
                else
                {
                    return false;
                }
            case 3:
                if (CooldownD)
                {
                    CooldownD = false;
                    return true;
                }
                else
                {
                    return false;
                }
            case 4:
                if (CooldownSI)
                {
                    CooldownSI = false;
                    StartCoroutine(TimeCooldown(4, TimeSI));
                    return true;
                }
                else
                {
                    return false;
                }
            default:
                return false;
        }
    }
    public IEnumerator TimeCooldown(int hability, float time)
    {
        switch (hability)
        {
            case 1:
                yield return new WaitForSeconds(time);
                CooldownQ = true;
                break;
            case 2:
                yield return new WaitForSeconds(time);
                CooldownE = true;
                break;
            case 3:
                yield return new WaitForSeconds(time);
                CooldownD = true;
                break;
            case 4:
                yield return new WaitForSeconds(time);
                CooldownSI = true;
                break;
            default:
                break;
        }
    }
}
