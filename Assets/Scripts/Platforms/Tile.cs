using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	Vector3 world;
	Vector2 SpriteSize;

	void Awake () {
		world = Camera.main.ScreenToWorldPoint(new Vector3 (Camera.main.pixelWidth
			, Camera.main.pixelHeight
			, 0));

		SpriteSize = new Vector2(0,0);

		SpriteRenderer sprite = GetComponent<SpriteRenderer>();
		if (sprite != null) {
			SpriteSize.x = sprite.bounds.size.x / transform.localScale.x;
			SpriteSize.y = sprite.bounds.size.y / transform.localScale.y;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < Camera.main.transform.position.x -world.x - SpriteSize.x / 2) {
			SimplePool.Despawn(gameObject);
		}
	}
}
