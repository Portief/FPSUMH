using UnityEngine;
using System.Collections;

public class DañoBala : MonoBehaviour{
    public int Daño = 10;
	// Use this for initialization
	void Start () {
        Destroy(this.gameObject,10f);
        //transform.rotation = rotacion;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
