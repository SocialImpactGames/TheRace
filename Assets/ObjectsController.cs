using UnityEngine;
using System.Collections;

public class ObjectsController : MonoBehaviour {
	public static void SpawnObjectInHand (Vector3 pos) {
		PhotonNetwork.Instantiate ("Object_Box", pos + (Vector3.left + Vector3.up) * 2, Quaternion.identity, 0);
	}
}
