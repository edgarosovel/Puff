using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

public class MovementFallingFloor : NetworkBehaviour {

	Rigidbody rb;
	float x, y;
	public float force = 10;

	void Start () {
		if (!isLocalPlayer) {
			Destroy (this);
			return;
		}
		rb = gameObject.GetComponent<Rigidbody> ();	
	}

	void Update () {
		if (GameManager.state!="playing") return;
		x = CrossPlatformInputManager.GetAxis("Horizontal");
		y = CrossPlatformInputManager.GetAxis("Vertical");
	}

	void FixedUpdate () {
		if (GameManager.state!="playing") return;
		if (x == 0 && y == 0) return;
		rb.AddForce (new Vector3(x,0,y) * force);
	}
}
