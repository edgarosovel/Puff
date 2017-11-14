using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour {

	public TileFallingFloor [] tiles;

	public void drop_tiles(int how_many, float time, int [] tiles){
		for (int i=0; i<how_many; i++){
			this.tiles[tiles[i]].drop(time);
		}
	}

}
