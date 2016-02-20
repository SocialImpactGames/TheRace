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
	PlayerNetwork network;

	void Start() {
		controller = GetComponent<PlayerMovmentLogic> ();
		network = GetComponent<PlayerNetwork> ();

		if (network.isMine ())
			Camera.main.GetComponent<SmoothCamera2D> ().target = gameObject.transform;
		else {
			transform.GetChild (0).gameObject.SetActive (false);
		}

		gravity = -(2 * jumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
	}

	void Update() {
		if (GameMaster.Instance.state != GameMaster.GameState.Playing)
			return;

		if(network.isMine() == false)
			return;

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
		return (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0)) && network.isMine ();
	}
}