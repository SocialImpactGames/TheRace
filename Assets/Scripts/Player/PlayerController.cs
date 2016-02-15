using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	Player player;

	void Awake(){
		player = GetComponent<Player> ();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			player.Jump ();
		}
	}
}
