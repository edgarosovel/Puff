using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRow : MonoBehaviour {

	public Text place, nametext, points;
	public Image place_color;

	public void set_values(string place, string name, string points, Color32 color){
		this.place_color.color = color;
		this.place.text = place;
		this.nametext.text = name;
		this.points.text = points;
	}
}
