using UnityEngine;
using System.Collections;

public class MultiBlockPlatform : MonoBehaviour {
	public GameObject TilePrefap;

	public Sprite Left;
	public Sprite Middle;
	public Sprite Right;

	[Range(0,200)]
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
	}

	void CreateTile(Sprite sprite, int index){
		GameObject go = SimplePool.Spawn(TilePrefap, Vector3.zero, Quaternion.identity);
		go.transform.SetParent (transform);
		go.name = sprite.name + index;
		go.GetComponent<SpriteRenderer> ().sprite = sprite;

		go.transform.localPosition = new Vector2 (index,0);
		go.transform.localRotation = Quaternion.identity;
	}
}
