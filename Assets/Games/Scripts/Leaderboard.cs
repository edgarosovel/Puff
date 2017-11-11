using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour {

	public GameObject row, nextgame_btn, scores_container;
	public Text title;


	public void config (List<KeyValuePair<int,string>> scores, string title, bool is_minigame, bool activate_nextgame_btn){
		this.title.text = title;
		if (activate_nextgame_btn) nextgame_btn.SetActive (true);
		int i = 1;
		scores.ForEach( x => {
			GameObject player = Instantiate(row) as GameObject;
			player.transform.SetParent (scores_container.transform, false);
			ScoreRow score_row = player.GetComponent<ScoreRow>();
			string points;
			if (is_minigame){
				switch (i){
					case 1: points = "10 pts";
					break;
					case 2: points = "7 pts";
					break;
					case 3: points = "5 pts";
					break;
					case 4: points = "3 pts";
					break;
					case 5: points = "2 pts";
					break;
					case 6: points = "0 pts";
					break;
					default: points = "0 pts";
					break;
				}
			}else points = x.Key.ToString() + " pts";
			score_row.set_values (i.ToString() + "°", x.Value.ToString(), points);
		});
	}

}
