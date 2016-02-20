using UnityEngine;
using System.Collections;

public class PlatformCreatorMaster : MonoBehaviour {
	public GameObject TilePrefap;
	public GameObject ExitPrefap;

	public Sprite Left;
	public Sprite Middle;
	public Sprite Right;

	int distanceCovered;
	Vector3 worldSize;

	int TargetDistance = 300;

	void Start () {
		distanceCovered = 22;
		 worldSize = Camera.main.ScreenToWorldPoint(new Vector3 (Camera.main.pixelWidth
			, Camera.main.pixelHeight
			, 0));
	}
	
	void Update () {
		if (GetMostRight () > distanceCovered) {
			CreateRandomPlatformStartingFromX (distanceCovered);// + Random.Range (1, 5));
		}
	}

	void CreateRandomPlatformStartingFromX(int x){
		if (distanceCovered < TargetDistance) {
			CreateTiles (4);
			distanceCovered = x + 6;
		} else {
			CreateWinningBlock ();
		}
	}

	float GetMostRight(){
		return Camera.main.transform.position.x + worldSize.x;
	}

	[ContextMenu("CreateTiles")]
	public void CreateTiles(int count){
		int y = Random.Range (-3, 1);

		CreateTile (Left, 0, y);

		for (int i = 0; i < count; i++) {
			CreateTile (Middle, i + 1, y);
		}

		CreateTile (Right, count + 1, y);
	}

	void CreateTile(Sprite sprite, int index, int y){
		GameObject go = SimplePool.Spawn(TilePrefap, Vector3.zero, Quaternion.identity);
		go.transform.parent = GameObject.FindObjectOfType<PlatformCreatorMaster>().transform;
		go.name = sprite.name + index;
		go.GetComponent<SpriteRenderer> ().sprite = sprite;

		go.transform.position = new Vector2 (distanceCovered + 1 + index, y);
		go.transform.localRotation = Quaternion.identity;
	}

	void CreateWinningBlock(){
		int y = Random.Range (-3, 1);

		CreateTile (Left, 0, y);

		for (int i = 0; i < 10; i++) {
			CreateTile (Middle, i + 1, y);
		}

		CreateTile (Right, 10 + 1, y);


		GameObject go = SimplePool.Spawn(ExitPrefap, Vector3.zero, Quaternion.identity);
		go.transform.parent = GameObject.FindObjectOfType<PlatformCreatorMaster>().transform;

		go.transform.position = new Vector2 (distanceCovered + 1 + 5, y + 2);
		go.transform.localRotation = Quaternion.identity;

		distanceCovered += 100;
	}
}
