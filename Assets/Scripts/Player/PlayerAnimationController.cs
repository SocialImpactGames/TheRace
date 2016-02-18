using UnityEngine;
using System.Collections;

public class PlayerAnimationController : MonoBehaviour {
	Animator aniamtor;
	Player player;

	void Start () {
		aniamtor = GetComponent<Animator> ();
		player = GetComponent<Player> ();
	}
	
	void Update () {
		print (player.velocity.y);
		aniamtor.SetFloat ("YVelocity", player.velocity.y);
		aniamtor.SetBool("OnGround", player.IsOnGround());
	}
}
