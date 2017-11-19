using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FallingFloorManager : NetworkBehaviour {

	public GameManager game_manager;
	int[]tiles = {0,1,2,3,4,5,6,7,8,9};
	FloorManager floor_manager;
	float time;
	int how_many, max_time;
	bool not_done = true;

	void Start () {
		floor_manager = FindObjectOfType<FloorManager> ();
		max_time = game_manager.gameplay_time;
	}

	void Update(){
		if (isServer && isLocalPlayer && GameManager.state=="playing" && not_done) {
			drop_tiles();
			not_done = false;
		}
	}


	void drop_tiles () {
		if (GameManager.state != "playing") return;
	
		time = map (game_manager.time, 0f, max_time, 3f, 7f);
		how_many = Mathf.RoundToInt (map (game_manager.time, 0f, max_time, 8f, 1f));

		for (int i = 0; i < 9; i++) {
			int c = Random.Range(0, 9-i);
			int t = tiles[i];
			tiles[i] = tiles[i+c];
			tiles[i+c] = t;
		}

		RpcDropTiles (how_many, time, tiles);

		Invoke ("drop_tiles", time + time * 30 / 100);
	}

	[ClientRpc]
	void RpcDropTiles(int how_many, float time, int[]tiles){
		floor_manager.drop_tiles (how_many,time,tiles);
	}

	float map (float x, float from_min, float from_max, float to_min, float to_max){
		return (x - from_min) * (to_max - to_min) / (from_max - from_min) + to_min;
	}
}
