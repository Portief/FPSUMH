using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ClassIngenieroElectronico : NetworkBehaviour, BaseClass {

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

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Q();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            E();
        }
        else if (Input.GetButtonDown("Fire2"))
        {
            D();
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SI();
        }
    }
    [Command]
    void CmdGrenadeServer()
    {
        GameObject instanceGrenade = (GameObject)Instantiate(Grenade, SpawnGrenade.transform.position, Quaternion.identity);
        instanceGrenade.GetComponent<Rigidbody>().velocity =SpawnGrenade.transform.TransformDirection( new Vector3(0, Mathf.Sin(Angle * Mathf.Deg2Rad) * SpeedGrenade, Mathf.Cos(Angle * Mathf.Deg2Rad) * SpeedGrenade));
        RpcBulletClient();
    }
    [ClientRpc]
    void RpcBulletClient()
    {
        if (!isServer) {
            GameObject instanceGrenade = (GameObject)Instantiate(Grenade, SpawnGrenade.transform.position, Quaternion.identity);
            instanceGrenade.GetComponent<Rigidbody>().velocity = SpawnGrenade.transform.TransformDirection(new Vector3(0, Mathf.Sin(Angle * Mathf.Deg2Rad) * SpeedGrenade, Mathf.Cos(Angle * Mathf.Deg2Rad) * SpeedGrenade));
        }
    }
    public void Q()
    {
        if(Cooldown(1))
        {
            CmdGrenadeServer();
        }
    }
    public void E()
    {
        if (Cooldown(2))
        {
            Debug.Log("E");
        }
    }
    public void D()
    {
        if (Cooldown(3))
        {
            Debug.Log("D");
        }
    }
    public void SI()
    {
        if (Cooldown(4))
        {
            Debug.Log("Click D");
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
                    StartCoroutine(TimeCooldown(1,TimeQ));
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
                    StartCoroutine(TimeCooldown(2,TimeE));
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
                    StartCoroutine(TimeCooldown(3, TimeD));
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
