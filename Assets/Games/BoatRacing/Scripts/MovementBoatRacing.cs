using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MovementBoatRacing : NetworkBehaviour {

	int touch_count;
	float startTime,smooth,angleVelocity,_toAngle,zAngle,cam_offset;
	Vector3 _from, _to, velocity,velocity_cam;
	EmitParticles particles;
	Camera cam;
	GameManager gameManager;

	void Start () {
		if (!isLocalPlayer) {
			Destroy (this);
			return;
		}
		gameManager = GetComponent<GameManager>();
		particles = GetComponent<EmitParticles> ();
		cam = Camera.main;
		cam_offset = cam.transform.position.x - gameObject.transform.position.x;
		velocity_cam = velocity = Vector3.zero;
		smooth = 0.5f;
		touch_count = 0;
		_to = transform.position;
	}
		
	void Update () {
		if (GameManager.state!="playing") return;
		transform.position = Vector3.SmoothDamp(transform.position, _to, ref velocity, smooth);
		if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetKeyDown("space")){
			touch_count++;
			gameManager.score = touch_count;
			if(touch_count==gameManager.pointsToWin){
				gameManager.CmdFinishGame ();
			}
			if (isServer) particles.RpcEmitParticles ();
			else particles.CmdEmitParticles ();
			_to = new Vector3 (_to.x + 0.1f, transform.position.y, transform.position.z);
		}

		cam.transform.position = Vector3.SmoothDamp(cam.transform.position,  new Vector3 
			(gameObject.transform.position.x+cam_offset,cam.transform.position.y,cam.transform.position.z), ref velocity_cam, 5f);
	}
}