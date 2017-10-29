using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EmitParticles : NetworkBehaviour {

	ParticleSystem part_sys;

	void Start () {
		part_sys = gameObject.GetComponent<ParticleSystem>();	
	}

	[Command]
	public void CmdEmitParticles(){
		RpcEmitParticles ();
	}

	[ClientRpc]
	public void RpcEmitParticles(){
		part_sys.Play ();
	}
}
