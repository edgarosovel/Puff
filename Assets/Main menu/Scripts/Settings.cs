using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

	public GameObject panel_settings;

	public void open_settings () {
		panel_settings.SetActive (true);
		panel_settings.GetComponent<Panel_multiplayer_name>().activate();
	}

}
