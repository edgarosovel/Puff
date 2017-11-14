using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CollisionsFallingFloor : NetworkBehaviour {

	public GameManager gameManager;
	int numberOfDead = 1;
	ParticleSystem ps;


	void Start () {
		ps = gameObject.GetComponent<ParticleSystem>();	
	}


	[ServerCallback]
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "caida" && GameManager.state=="playing"){
			RpcDestroyPlayer (numberOfDead);
			numberOfDead++;
			//if (numberOfDead == gameManager.get_number_of_players () && GameManager.state=="playing") gameManager.CmdFinishGame ();
		}
	}

	[ClientRpc]
	void RpcDestroyPlayer(int numberOfDead){
		ps.Play ();
		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<Rigidbody> ().isKinematic = true;
		GetComponent<SphereCollider> ().enabled = false;
		gameObject.transform.GetChild (0).gameObject.SetActive (false);
		gameObject.transform.GetChild(1).gameObject.SetActive(false);
		if (isLocalPlayer) gameManager.score = numberOfDead;
	}
}
