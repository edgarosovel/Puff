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
	public GameObject nombre, arrow; 
	GameManager gameManager;
	List <int[]> scores = new List<int[]>();

	void Start () {
		if (!isLocalPlayer) {
			arrow.SetActive (false);
			Destroy (this);
			return;
		}
		particles = GetComponent<EmitParticles> ();
		nombre.SetActive (false);
		cam = Camera.main;
		cam_offset = cam.transform.position.x - gameObject.transform.position.x;
		velocity_cam = velocity = Vector3.zero;
		smooth = 0.5f;
		touch_count = 0;
		_to = transform.position;
	}
		
	void Update () {
		if (gameManager == null) {
			if (GameObject.FindWithTag("GameManager")!=null)
			gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
			return;
		}
		if (gameManager.state!="playing") return;
		transform.position = Vector3.SmoothDamp(transform.position, _to, ref velocity, smooth);
		if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetKeyDown("space")){
			touch_count++;
			if(touch_count==200){
				//CmdFinish ();
			}
			if (isServer) particles.RpcEmitParticles ();
			else particles.CmdEmitParticles ();
			_to = new Vector3 (_to.x + 0.1f, transform.position.y, transform.position.z);
		}

		cam.transform.position = Vector3.SmoothDamp(cam.transform.position,  new Vector3 
			(gameObject.transform.position.x+cam_offset,cam.transform.position.y,cam.transform.position.z), ref velocity_cam, 5f);
	}


	[Command]
	public void CmdFinish(){
		int [] s = {base.connectionToClient.connectionId, touch_count};
		scores.Add (s);
		RpcFinish ();
	}


	[ClientRpc]
	void RpcFinish(){
		scores.Sort ();
		//gameManager.setFinish (scores);
	}
		
}