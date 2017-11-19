using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Prototype.NetworkLobby;
using UnityEngine.SceneManagement;

public class NextGameButton : NetworkBehaviour {
	Text t;
	string next;

	void Start(){
		t = transform.GetChild (0).GetComponent<Text> ();
		LobbyManager.s_Singleton.SetLastScene(SceneManager.GetActiveScene().name);
		next = LobbyManager.s_Singleton.get_next_scene ();
		if (next == null)
			t.text = "Back to lobby";
	}

	public void next_scene()
	{
		if (next != null)
			LobbyManager.s_Singleton.ServerChangeScene (next);
		else {
			LobbyManager.s_Singleton.SetLastScene(LobbyManager.s_Singleton.lobbyScene);
			LobbyManager.s_Singleton.ServerReturnToLobby ();
		}
	}
}
