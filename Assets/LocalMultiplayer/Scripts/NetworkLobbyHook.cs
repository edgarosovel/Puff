using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook 
{
    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
		GameManager player_game_manager = gamePlayer.GetComponent<GameManager> ();

		player_game_manager.skin = lobby.skin;
		player_game_manager.playerName = lobby.playerName;
    }
}
