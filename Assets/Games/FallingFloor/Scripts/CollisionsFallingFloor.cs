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
		if(col.gameObject.tag == "caida") {
			RpcDestroyPlayer ();
			if (GameManager.state=="playing"){
				RpcSetPlayerScore (numberOfDead);
				numberOfDead++;
				if (numberOfDead >= gameManager.get_number_of_players ()) 
					gameManager.CmdFinishGame ();
			}
		}
	}

	[ClientRpc]
	void RpcDestroyPlayer(){
		ps.Play ();
		GetComponent<MeshRenderer> ().enabled = false;
		GetComponent<Rigidbody> ().isKinematic = true;
		GetComponent<SphereCollider> ().enabled = false;
		gameObject.transform.GetChild (0).gameObject.SetActive (false);
		gameObject.transform.GetChild(1).gameObject.SetActive(false);
	}

	[ClientRpc]
	void RpcSetPlayerScore(int numberOfDead){
		gameManager.score = numberOfDead;
	}
}
