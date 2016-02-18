using UnityEngine;
using System.Collections;

[RequireComponent (typeof (PlayerMovmentLogic))]
public class Player : MonoBehaviour {

	public float jumpHeight = 4;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	public float moveSpeed = 8;

	float gravity;
	float jumpVelocity;
	public Vector3 velocity;
	float velocityXSmoothing;

	PlayerMovmentLogic controller;

	void Start() {
		controller = GetComponent<PlayerMovmentLogic> ();

		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		print ("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
	}

	void Update() {

		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}

		Vector2 input = Vector2.right;

		if ( ShouldJump() && CanJump()) {
			velocity.y = jumpVelocity;
		}

		float targetVelocityX = input.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (IsOnGround())?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
		controller.Move (velocity * Time.deltaTime);
	}

	public bool CanJump(){
		return IsOnGround ();
	}

	public bool IsOnGround(){
		return controller.collisions.below;
	}

	bool ShouldJump(){
		return Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0);
	}
}