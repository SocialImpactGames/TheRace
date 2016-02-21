using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//TODO: Refactor ( Separate Model from View )
public class PlatformsMaster : MonoBehaviour {
	public GameObject TilePrefap;
	public GameObject ExitPrefap;

	public GameObject Star;

	public Sprite Left;
	public Sprite Middle;
	public Sprite Right;

	int distanceCovered;

	int TargetDistance = 50;

	Dictionary<int, Vector3> tilesX;

	public static PlatformsMaster Instance; 

	void Start () {
		if (Instance == null)
			Instance = this;
		else
			Destroy (gameObject);

		tilesX = new Dictionary<int, Vector3> ();
		distanceCovered = 0;
		CreateTiles (22, false);
	}

	public void CreateGameTiles(){
		while (TargetDistance > distanceCovered) {
			CreateTiles (Random.Range (4, 12));
			distanceCovered += Random.Range (2, 5); // Gap
		}

		CreateWinningPlatform ();
	}

	void CreateTiles(int count, bool randomY = true){
		int y = 0;
		if(randomY)
			Random.Range (-1, 4);

		CreateTile (Left, y);

		for (int i = 0; i < count; i++) {
			CreateTile (Middle, y);
		}

		CreateTile (Right, y);
	}

	void CreateTile(Sprite sprite, int y){
		GameObject go = SimplePool.Spawn(TilePrefap, Vector3.zero, Quaternion.identity);
		go.transform.parent = GameObject.FindObjectOfType<PlatformsMaster>().transform;
		go.name = sprite.name + distanceCovered;
		go.GetComponent<SpriteRenderer> ().sprite = sprite;

		go.transform.position = new Vector2 (distanceCovered, y);
		go.transform.localRotation = Quaternion.identity;
		tilesX.Add (distanceCovered, go.transform.position);
		distanceCovered++;

		if (Random.Range (0, 12) == 5) {
			GameObject star = SimplePool.Spawn(Star, new Vector2 (distanceCovered , y + Random.Range(2,5)), Quaternion.identity);
		}
	}

	void CreateWinningPlatform(){
		CreateTiles (10, false);

		GameObject go = SimplePool.Spawn(ExitPrefap, Vector3.zero, Quaternion.identity);
		go.transform.parent = GameObject.FindObjectOfType<PlatformsMaster>().transform;

		go.transform.position = new Vector2 (distanceCovered - 4, 0);
		go.transform.localRotation = Quaternion.identity;

		distanceCovered += 100;
	}

	public Vector3 GetValidTileTransformAfterX(int x){
		Vector3 t = Vector3.zero;
		int counter = 10;
		while (t == Vector3.zero && counter-- > 0)
			tilesX.TryGetValue (++x,out t);
		return t;
	}
}
