using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class GameManager : NetworkBehaviour {

	public GameObject puff,playerNameObj, arrow;

	[SyncVar]
	public string playerName;
	[SyncVar]
	public string skin;
	MatchData match_data;

	ScoreTableManager score_table_manager;
	public bool higher_score_wins;
	public static string state;
	float time;
	public int gameplay_time, pointsToWin; 
	[HideInInspector] public int score;
	int floored_time;
	GameObject ui, start_info, loading, end_info;
	Text timer, counter;
	public GameObject  score_table;

	void Start(){
		if (!isLocalPlayer) {
			arrow.SetActive (false);
		} else {
			playerNameObj.SetActive (false);
		}
		match_data = FindObjectOfType<MatchData> ();
		if (match_data.player_exists (SystemInfo.deviceUniqueIdentifier+playerName)) {
			string[] info = match_data.get_player_info (SystemInfo.deviceUniqueIdentifier+playerName);
			set_up_player (info [1], info [0]); 
		} else if(isLocalPlayer) {
			Invoke ("add_player", 2f);
		}
		set_up_player (playerName, skin);

		ui = GameObject.FindWithTag("ui");
		timer = ui.transform.GetChild (0).gameObject.GetComponent<Text>();
		start_info = ui.transform.GetChild(1).gameObject;
			loading = start_info.transform.GetChild (0).gameObject;
			counter = start_info.transform.GetChild (1).gameObject.GetComponent<Text>();
		end_info = ui.transform.GetChild(2).gameObject;

		if (!isServer)return;
		time = 7;
		Invoke ("StartCounter",6.5f);
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
	public void CmdFinishGame(){
		RpcUpdateState ("finished", 0);
		set_minigame_scores ();
		RpcShowScores ();
	}

	void set_minigame_scores(){
		foreach (var player in match_data.players.Values) {
			player.game_manager.RpcSetMinigameScore();
		}
	}

	[ClientRpc]
	void RpcShowScores(){
		Invoke ("show_scores", 3f);
	}

	void show_scores(){
		score_table_manager = end_info.GetComponent<ScoreTableManager> ();
		update_global_points();
		show_minigame_scores ();
		Invoke ("show_global_scores", 4f);
	}

	void update_global_points(){
		List<KeyValuePair<int,string>> minigame_scores = match_data.get_minigame_for_points (higher_score_wins);
		int i = 1;
		foreach (var player in minigame_scores) {
			Debug.Log ("Minigame table: "+player.Value+ "score "+ player.Key.ToString());
			int points = 0;
			switch (i){
			case 1: points = 10;
				break;
			case 2: points = 7;
				break;
			case 3: points = 5;
				break;
			case 4: points = 3;
				break;
			case 5: points = 2;
				break;
			case 6: points = 0;
				break;
			default: points = 0;
				break;
			}
			match_data.add_global_points (player.Value, points);
			i++;
		}
	}

	void show_minigame_scores (){
		score_table_manager.create_minigame_score_table (match_data.get_minigame_scores (higher_score_wins), "This game scores");
	}

	void show_global_scores(){
		score_table_manager.destroy_minigame_score_table();
		List<KeyValuePair<int,string>> global_scores = match_data.get_global_leaderboard();
		score_table_manager.create_global_score_table (global_scores, "Global scores", isServer);
	}



	[ClientRpc]
	void RpcSetMinigameScore(){
		if (isLocalPlayer) {
			CmdSetMinigamePoints (SystemInfo.deviceUniqueIdentifier+playerName, score);
		}
	}
		
	void StartCounter(){
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
			if (time > 4) {
				loading.SetActive (false);
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
				start_info.SetActive (false);
				arrow.SetActive (false);
				playerNameObj.SetActive (false);
			}
		} else if (state == "playing") {
			if (time == 0) {
				if (isServer && isLocalPlayer) {
					CmdFinishGame ();
				}
			} else {
				timer.text = time.ToString ();
			}
		} else if (state == "finished") {
			timer.gameObject.SetActive (false);
			end_info.SetActive (true);
		}
			
	}


	///// MATCH DATA FUNCTIONS /////
	void add_player(){
		if (isServer) RpcAddPlayer (SystemInfo.deviceUniqueIdentifier + playerName, playerName, skin, 0, 0);
		else CmdAddPlayer (SystemInfo.deviceUniqueIdentifier+playerName, playerName, skin, 0, 0);
	}

	void set_up_player(string name, string skin){
		playerNameObj.GetComponent<TextMesh> ().text = name;
		puff.GetComponent<Renderer>().material = Resources.Load(skin, typeof(Material)) as Material;
	}

	public List<KeyValuePair<int,string>> get_minigame (bool higher_score_wins){
		return match_data.get_minigame_scores (higher_score_wins);
	}

	public int get_number_of_players(){
		return match_data.players.Count;
	}

	[Command]
	public void CmdAddPlayer (string id, string playerName, string skin, int global_points, int minigame_points){
		RpcAddPlayer (id, playerName, skin, global_points, minigame_points);
	}

	[ClientRpc]
	public void RpcAddPlayer (string id, string playerName, string skin, int global_points, int minigame_points){
		match_data.add_player (id, playerName, skin, global_points, minigame_points);
		if (isServer) match_data.set_game_manager (id, this);
	}

	[Command]
	public void CmdRemovePlayer (string id){
		RpcRemovePlayer (id);
	}

	[ClientRpc]
	public void RpcRemovePlayer (string id){
		match_data.remove_player (id);
	}

	[Command]
	public void CmdAddGlobalPoints (string id, int points){
		RpcAddGlobalPoints (id, points);
	}

	[ClientRpc]
	public void RpcAddGlobalPoints  (string id, int points){
		match_data.add_global_points (id, points);
	}

	[Command]
	public void CmdSetMinigamePoints (string id, int points){
		RpcSetMinigamePoints (id, points);
	}

	[ClientRpc]
	public void RpcSetMinigamePoints  (string id, int points){
		match_data.set_minigame_points (id, points);
	}
}
