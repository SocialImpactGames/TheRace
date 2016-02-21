using UnityEngine;
using System.Collections;

public class GameNetworkMaster : Photon.PunBehaviour {


	public override void OnJoinedRoom(){
		PhotonNetwork.Instantiate ("Player", new Vector3(PhotonNetwork.room.playerCount,2,0), Quaternion.identity, 0);
		StartGame ();
	}

	public override void OnJoinedLobby()
	{
		JoinRoom ();	
	}

	public override void OnConnectedToMaster()
	{
		JoinRoom ();
	}

	void JoinRoom(){
		RoomOptions ro =  new RoomOptions() { isVisible = true, maxPlayers = 4 };
		PhotonNetwork.JoinOrCreateRoom ("race", ro, TypedLobby.Default);
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
