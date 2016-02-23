using UnityEngine;
using System.Collections;

public class GameNetworkMaster : Photon.PunBehaviour {

	void Awake(){
		PhotonNetwork.Instantiate ("Player", new Vector3(PhotonNetwork.room.playerCount,2,0), Quaternion.identity, 0);
	}

	public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
	{
		if (PhotonNetwork.playerList.Length >= 2) {
			PhotonNetwork.room.open = false;
			StartGame ();
		}
	}

	void StartGame(){
		int seed = Random.Range (10000, 1000000);
		photonView.RPC ("PunRPC_StartGame", PhotonTargets.All, seed);
	}

	[PunRPC]
	void PunRPC_StartGame(int seed){
		Random.seed = seed;
		GameMaster.Instance.StartGame ();
	}
}
