using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Prototype.NetworkLobby;

public class MainMenuLoadScenes : MonoBehaviour {

	void Start(){
		LobbyManager lobby_man = GameObject.FindObjectOfType<LobbyManager>();
		if (lobby_man != null) Destroy (lobby_man.gameObject);	
	}

	public void to_skins(){
		SceneManager.LoadScene (3);
	}

	public void to_local_multiplayer(){
		SceneManager.LoadScene (1);
	}

}