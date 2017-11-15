using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class NextGameButton : NetworkBehaviour {

	public void next_scene()
	{
		LobbyManager.s_Singleton.ServerChangeScene(LobbyManager.s_Singleton.game_scenes[2]);
		//LobbyManager.s_Singleton.ServerReturnToLobby();
		//LobbyManager.s_Singleton.ServerChangeScene("FallingFloor");
	}
}
