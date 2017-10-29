using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GameManager : NetworkBehaviour {

	[SyncVar] public string state;
	public Text timer, counter;
	[SyncVar] float time_left,time_counter;
	public GameObject counterUI, title, info, end_info;

	[ServerCallback]
	void Start () {
		time_counter = 5;
		time_left = 5;
		RpcStartCounter ();
	}

	void Update(){
		if (state=="counter") {
			if (time_counter>4) {
				counter.text = "READY?";
			}
			else if (time_counter > 1) {
				counter.text = ((int)time_counter).ToString ();
			} else {
				counter.text = "GO!";
			}
			if (time_counter <= 0) {
				RpcStartGame ();
			}
			if (isServer) time_counter -= Time.deltaTime;
		}
		if (state=="playing") {
			timer.text = ((int)time_left+1).ToString ();
			if (time_left <= 0) {
				if (isServer) RpcSetFinish ();
			}
			if (isServer) time_left -= Time.deltaTime;
		}
	}

	[ClientRpc]
	void RpcStartCounter(){
		StartCoroutine("startGame");
	}

	IEnumerator startGame() {
		yield return new WaitForSeconds(4);
		title.SetActive (false);
		info.SetActive (false);
		if (isServer) state = "counter";
	}
		
	[ClientRpc]
	void RpcSetFinish(){
		if (isServer) state = "finished";
		timer.gameObject.SetActive (false);
		end_info.SetActive (true);
	}

	[ClientRpc]
	void RpcStartGame(){
		if (isServer) state = "playing";
		counterUI.SetActive (false);
	}
		
}
