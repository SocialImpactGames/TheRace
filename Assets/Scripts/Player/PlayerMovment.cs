using UnityEngine;
using System.Collections;

public class PlayerMovment : MonoBehaviour {
	Rigidbody2D rb;

	bool isJumping, isFalling;
	bool isDoupleJumping;


	public float Speed = 3f;
	public float JumpSpeed = 9f;


	void Awake () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void FixedUpdate () {
		rb.velocity = new Vector2 (Speed, rb.velocity.y);

		if (Mathf.Abs (rb.velocity.y) < 0.01f) {
			isJumping = false;
			isDoupleJumping = false;
		}

		isFalling = rb.velocity.y < 0;
	}

	public void Jump(){
		if (isJumping)
			isDoupleJumping = true;
		else 
			isJumping = true;
		rb.velocity = new Vector2 (rb.velocity.x, JumpSpeed);
	}

	public bool CanJump(){
		return isJumping == false || ( isDoupleJumping == false && isFalling == false);
	}
}
