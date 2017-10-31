using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuLoadScenes : MonoBehaviour {

	public void to_skins(){
		SceneManager.LoadScene (3);
	}

	public void to_local_multiplayer(){
		SceneManager.LoadScene (1);
	}

}