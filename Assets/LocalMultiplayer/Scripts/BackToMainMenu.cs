using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour {

	public void back_to_main_menu(){
		SceneManager.LoadScene (0);
	}
}
