﻿using UnityEngine;
using System.Collections;

public class GameNetworkMaster : Photon.PunBehaviour {


	public override void OnJoinedRoom(){
		print ("JoinedRoom");
		PhotonNetwork.Instantiate ("Player", Vector3.zero, Quaternion.identity, 0);
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

	[ContextMenu("StartGame")]
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
