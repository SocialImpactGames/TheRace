using UnityEngine;
using System.Collections;

public class PlayerNetwork : Photon.PunBehaviour {
	Player player;
	PlayerMovmentLogic ctrl;

	Vector3 pos;

	void Awake(){
		player = GetComponent<Player> ();
		ctrl = GetComponent<PlayerMovmentLogic> ();
	}

	public bool isMine(){
		return photonView.isMine;
	}

	void Update(){
		if (isMine() == false) {
			transform.position = pos;//Vector3.Lerp(transform.position, pos, 1 * Time.deltaTime);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting) {
			stream.SendNext (transform.position);

			stream.SendNext (player.velocity);
			stream.SendNext (ctrl.collisions.below);
		} else {
			pos = (Vector3)stream.ReceiveNext ();

			player.velocity = (Vector3)stream.ReceiveNext ();
			ctrl.collisions.below = (bool)stream.ReceiveNext ();
		}
	}
}
