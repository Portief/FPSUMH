using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Grenade : NetworkBehaviour {
    private Rigidbody myrigidbody;
    public int damage = 10;
	// Use this for initialization
	void Start () {
        myrigidbody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        myrigidbody.AddForce(Physics.gravity*myrigidbody.mass*GlobalVariables.multiplier,ForceMode.Acceleration);
	}
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Health>()!=null)
        {
            if (isServer)
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
        //Destroy(this.gameObject);
    }
}
