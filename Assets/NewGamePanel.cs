using UnityEngine;
using System.Collections;

public class NewGamePanel : Photon.PunBehaviour {

	public void JoinRoom(){
		PhotonNetwork.JoinOrCreateRoom (PlayerPrefs.GetString("nickname", ""), new RoomOptions(){maxPlayers = 2, isVisible = true}, TypedLobby.Default);
	}
}
