using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DañoBala : NetworkBehaviour{
    public int Daño = 10;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject,10f);
        //transform.rotation = rotacion;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision Colision)
    {
        if (Colision.gameObject.GetComponent<Vida>()!=null)
        {
            Colision.gameObject.GetComponent<Vida>().TakeDamage(Daño);
            Destroy(this.gameObject);
        }
    }
}
