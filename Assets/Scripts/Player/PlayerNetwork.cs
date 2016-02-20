using UnityEngine;
using System.Collections;

public class PlayerNetwork : Photon.PunBehaviour {
	public bool isMine(){
		return photonView.isMine;
	}
}
