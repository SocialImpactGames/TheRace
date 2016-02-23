using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class JoinRoom : MonoBehaviour {
	public string RoomName;
	public Text text;

	public void Join () {
		PhotonNetwork.JoinRoom (RoomName);
	}
	
	public void SetRoomName (string roomName) {
		RoomName = roomName;
		text.text = RoomName;
	}
}
