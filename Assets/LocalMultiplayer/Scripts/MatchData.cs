using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchData : MonoBehaviour {

	int players_added=0;

	public Dictionary <string, PlayerInfo> players = new Dictionary<string, PlayerInfo> ();
	public static MatchData instance;

	void Awake (){
		instance = this;
	}

	public void add_player (string id, string playerName, string skin, int global_points, int minigame_points){
		if (players.ContainsKey(id)) return;
		Debug.Log ("adding " + id);
		players.Add(id, new PlayerInfo(playerName, skin, global_points, minigame_points));
	}

	public void remove_player (string id){
		players.Remove (id);
	}

	public bool player_exists(string id){
		return players.ContainsKey(id);
	}

	public void add_global_points(string id, int points){
		if (!players.ContainsKey(id)) return;
		players[id].global_points += points;
	}

	public void set_minigame_points(string id, int points){
		if (!players.ContainsKey(id)) return;
		players[id].minigame_points = points;
		players_added++;
		if (players_added == players.Count) {
			players_added = 0;
			// update global scores
			// comando para mostrar tabla
		}
	}

	public string[] get_player_info(string id){
		if (!players.ContainsKey(id)) return null;
		return new string[2]{players[id].skin,players[id].playerName};
	}

	public List<KeyValuePair<int,string>> get_global_leaderboard (){
		List<KeyValuePair<int,string>> leaderboard = new List<KeyValuePair<int,string>>();
		foreach(var player in players.Values){
			leaderboard.Add (new KeyValuePair<int, string> (player.global_points, player.playerName));
		}
		leaderboard.OrderByDescending (x => x.Key).ToList();
		return leaderboard;
	}

	public List<KeyValuePair<int,string>> get_minigame_for_points (bool higher_score_wins){
		List<KeyValuePair<int,string>> scores = new List<KeyValuePair<int,string>>();
		foreach(var player in players){
			scores.Add (new KeyValuePair<int, string> (player.Value.minigame_points, player.Key));
		}
		if (higher_score_wins) scores.OrderByDescending (x => x.Key).ToList();
		else scores.OrderBy (x => x.Key).ToList();
		return scores;
	}

	public List<KeyValuePair<int,string>> get_minigame_scores (bool higher_score_wins){
		List<KeyValuePair<int,string>> scores = new List<KeyValuePair<int,string>>();
		foreach(var player in players.Values){
			scores.Add (new KeyValuePair<int, string> (player.minigame_points, player.playerName));
		}
		if (higher_score_wins) scores.OrderByDescending (x => x.Key).ToList();
		else scores.OrderBy (x => x.Key).ToList();
		return scores;
	}

	public class PlayerInfo{
		public string playerName;
		public string skin;
		public int global_points;
		public int minigame_points;

		public PlayerInfo(string playerName, string skin, int global_points, int minigame_points){
			this.playerName = playerName;
			this.skin = skin;
			this.global_points = global_points;
			this.minigame_points = minigame_points;
		}
	}

}
