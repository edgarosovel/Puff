using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CollisionsKingOfTheHill : NetworkBehaviour {

	public GameManager gameManager;
	static int numberOfDead;
	ParticleSystem ps;


	void Start () {
		numberOfDead = 1;
		ps = gameObject.GetComponent<ParticleSystem>();	
		Invoke ("set_max_score", 3f);
	}

	void set_max_score(){
		gameManager.score = gameManager.get_number_of_players();	
	}

	[ServerCallback]
	void OnTriggerEnter(Collider col){
		if (!isServer) return;
		if(col.gameObject.tag == "caida") {
			RpcDestroyPlayer ();
			if (GameManager.state == "playing") {
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
