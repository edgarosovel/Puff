using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_multiplayer_name : MonoBehaviour {

	public InputField input_name;
	public Text aviso;
	string username;
	Color red;

	void Start () {
		if (!ZPlayerPrefs.HasKey("skin")) ZPlayerPrefs.SetInt ("skin",0);
		if (ZPlayerPrefs.HasKey ("username")) gameObject.SetActive (false);
		ColorUtility.TryParseHtmlString ("#EF4836", out red);
	}

	public void activate(){
		if (ZPlayerPrefs.HasKey("skin")) {
			input_name.text = ZPlayerPrefs.GetString ("username");
		}
	}

	public void save_name () {
		if (input_name.text == "") {
			aviso.text = "Multiplayer name!";
			aviso.color = red;
			return;
		}
		ZPlayerPrefs.SetString ("username",input_name.text);
		gameObject.SetActive (false);
	}

}
