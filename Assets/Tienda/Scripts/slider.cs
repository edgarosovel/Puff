using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slider : MonoBehaviour {

	public static int starNum;
	public Text texto;

	void Start () {
		ZPlayerPrefs.Initialize("N45v5$%sKL", SystemInfo.deviceUniqueIdentifier);
		starNum = ZPlayerPrefs.GetInt ("stars");
		gameObject.GetComponent<Slider> ().value = starNum;
		updateStarText ();
	}

	public void starNumber(){
		starNum = (int)gameObject.GetComponent<Slider> ().value;
		updateStarText ();
	}

	void updateStarText(){
		texto.text = "Number of stars: " + starNum; 
	}
}
