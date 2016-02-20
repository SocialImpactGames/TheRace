using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	Vector3 world;

	void Awake () {
		world = Camera.main.ScreenToWorldPoint(new Vector3 (Camera.main.pixelWidth
			, Camera.main.pixelHeight
			, 0));
	}
	
	void Update () {
		if (transform.position.x < Camera.main.transform.position.x -(1.4f * world.x) ) {
			SimplePool.Despawn(gameObject);
		}
	}
}
