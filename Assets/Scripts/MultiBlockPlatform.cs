using UnityEngine;
using System.Collections;

public class MultiBlockPlatform : MonoBehaviour {
	public Sprite Left;
	public Sprite Middle;
	public Sprite Right;

	[Range(0,20)]
	public int MiddleCount;

	void Awake () {
		CreateTiles ();
	}

	[ContextMenu("CreateTiles")]
	void CreateTiles(){
		
		CreateTile (Left, 0);

		for (int i = 0; i < MiddleCount; i++) {
			CreateTile (Middle, i + 1);
		}

		CreateTile (Right, MiddleCount + 1);

		BoxCollider2D collider = GetComponent<BoxCollider2D> ();
		collider.size = new Vector2( MiddleCount + 2, 1);
		collider.offset = new Vector2 ((MiddleCount / 2f)+0.5f,0);
	}

	void CreateTile(Sprite sprite, int index){
		GameObject go = new GameObject ();
		go.transform.SetParent (transform);
		go.name = sprite.name + index;
		go.AddComponent<SpriteRenderer> ().sprite = sprite;

		go.transform.localPosition = new Vector2 (index,0);
		go.transform.localRotation = Quaternion.identity;
	}
}
