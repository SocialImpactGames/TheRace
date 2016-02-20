using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	public static GameMaster Instance;
	public GameState state { get; protected set; }

	public enum GameState{
		Paused,
		Playing,
		End
	}

	void Awake(){
		if (Instance == null)
			Instance = this;
		else
			Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		state = GameState.Paused;
	}
		
	public void StartGame(){
		state = GameState.Playing;
	}
}
