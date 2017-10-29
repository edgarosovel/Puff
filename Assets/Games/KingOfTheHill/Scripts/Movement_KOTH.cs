﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Movement_KOTH : MonoBehaviour {

	Rigidbody rb;
	float x, y;
	public float force = 10;

	void Start () {
		rb = gameObject.GetComponent<Rigidbody> ();	
	}

	void Update () {
		x = CrossPlatformInputManager.GetAxis("Horizontal");
		y = CrossPlatformInputManager.GetAxis("Vertical");
	}

	void FixedUpdate () {
		rb.AddForce (new Vector3(x,0,y) * force);
//		rb.MovePosition(transform.position + new Vector3(0,y,x) * Time.deltaTime);
	}
}
