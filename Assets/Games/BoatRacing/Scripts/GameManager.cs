using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GameManager : NetworkBehaviour {

	public string state;
	public Text timer, counter;
	float time;
	public int gameplay_time; 
	[SyncVar] public int score;
	int floored_time;
	public GameObject ui;
	public GameObject counterUI, title, info, end_info;
	List<MovementBoatRacing> players = new List<MovementBoatRacing>();
	List <int> scores = new List <int>();

	void Start(){
		ui = GameObject.FindWithTag("ui");
		timer = ui.transform.GetChild (0).gameObject.GetComponent<Text>();
		counterUI = ui.transform.GetChild(1).gameObject;
		counter = counterUI.transform.GetChild (1).gameObject.GetComponent<Text>();
		title = counterUI.transform.GetChild (2).gameObject;
		info = counterUI.transform.GetChild (3).gameObject;
		end_info = ui.transform.GetChild(2).gameObject;

		if (!isServer)return;
		time = 5;
		Invoke ("RpcStartCounter",5f);
	}

	[ServerCallback]
	void Update () {
		if (state == "counter" || state == "playing") {
			time -= Time.deltaTime;
			if (floored_time != Mathf.FloorToInt(time)) {
				floored_time = Mathf.FloorToInt (time);
				RpcUpdateState (state, Mathf.FloorToInt(time));
			}
		}
	}
		

	[Command]
	public void CmdFinish(){
		RpcUpdateState ("finished", 0);
		RpcGetScores ();
	}

	[Command]
	public void CmdSendScores(){
		scores.Add(score);
	}

	[ClientRpc]
	void RpcGetScores(){
		CmdSendScores();
	}

	[ClientRpc]
	void RpcStartCounter(){
		title.SetActive (false);
		info.SetActive (false);
		if (isServer) {
			floored_time = Mathf.FloorToInt (time);
			time++;
			RpcUpdateState ("counter", floored_time);
		}
	}

	[ClientRpc]
	void RpcUpdateState(string new_state, int new_time){
		state = new_state;
		handleChanges (state, new_time);
	}

	void handleChanges(string state, int time){
		if (state == "counter") {
			if (time == 5) {
				counter.text = "READY?";
			} else if (time > 1) {
				counter.text = (time-1).ToString ();
			} else if (time == 1){
				counter.text = "GO!";
			} else{
				if (isServer) {	
					this.time = gameplay_time+1;
					floored_time = (gameplay_time);
					RpcUpdateState ("playing", gameplay_time);
				}
				counterUI.SetActive (false);
			}
		} else if (state == "playing") {
			if (time == 0) {
				if (isServer) {
					RpcUpdateState ("finished", 0);
					RpcGetScores ();
				}
			} else {
				timer.text = time.ToString ();
			}
		} else if (state == "finished") {
			timer.gameObject.SetActive (false);
			end_info.SetActive (true);
		}
			
	}
}
