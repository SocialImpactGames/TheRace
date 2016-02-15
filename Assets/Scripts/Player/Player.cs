using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	PlayerMovment movment;

	void Awake () {
		movment = GetComponent<PlayerMovment> ();
	}

	public void Jump(){
		if (CanJump ()) {
			movment.Jump ();
		}
	}

	bool CanJump(){
		return movment.CanJump ();
	}
}
