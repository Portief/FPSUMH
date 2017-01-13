using UnityEngine;
using System.Collections;

public class CenterMass : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Rigidbody myrigiboy = GetComponent<Rigidbody>();
        CharacterController mycharacterController = GetComponent<CharacterController>();
        myrigiboy.centerOfMass =mycharacterController.center;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
