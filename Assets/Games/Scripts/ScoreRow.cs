using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRow : MonoBehaviour {

	public Text place, nametext, points;

	public void set_values(string place, string name, string points){
		this.place.text = place;
		this.nametext.text = name;
		this.points.text = points;
	}
}
