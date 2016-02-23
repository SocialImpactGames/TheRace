using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ListGamesPanel : Photon.PunBehaviour {
	public GameObject btn;

	public GameObject tmp;

	public override void OnReceivedRoomListUpdate()
	{
		foreach (var room in PhotonNetwork.GetRoomList()) {
			GameObject go = Instantiate<GameObject> (btn);
			go.GetComponent<JoinRoom> ().SetRoomName (room.name);
			go.transform.SetParent (transform, false);
		}
	}

	public override void OnJoinedLobby ()
	{
		tmp.SetActive (false);
	}
}
