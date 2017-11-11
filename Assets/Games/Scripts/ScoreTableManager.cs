using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTableManager : MonoBehaviour {

	public GameObject score_table, counter;
	GameObject minigame_table, global_table;

	public void create_minigame_score_table(List<KeyValuePair<int,string>> scores, string title){
		counter.SetActive (false);
		minigame_table = Instantiate(score_table) as GameObject;
		minigame_table.transform.SetParent (gameObject.transform, false);
		minigame_table.GetComponent<Leaderboard> ().config(scores, title, true, false); 
	}

	public void destroy_minigame_score_table(){
		Destroy (minigame_table);
	}

	public void create_global_score_table(List<KeyValuePair<int,string>> scores, string title, bool activate_nextgame_btn){
		global_table = Instantiate(score_table) as GameObject;
		global_table.transform.SetParent (gameObject.transform, false);
		global_table.GetComponent<Leaderboard> ().config(scores, title, false, activate_nextgame_btn); 
	}

	public void destroy_global_score_table(){
		Destroy (global_table);
	}


}
