using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {
	enum InputButton{
		Jump,
		Attack
	}

	public enum InputSpace{
		Left,
		Right
	}

	bool shouldJump, shouldAttack;

	public static InputController Instance;

	void Awake(){
		if (Instance == null)
			Instance = this;
		else
			Destroy (gameObject);
	}


	public bool ShouldJump(){
		return Input.GetKeyDown (KeyCode.Space) || ButtonClick(InputButton.Jump);
	}

	public bool ShouldAttack(){
		return ButtonClick (InputButton.Attack);
	}

	bool ButtonClick(InputButton btn){		
		if (btn == InputButton.Jump) {
			return shouldJump;
		} else if (btn == InputButton.Attack) {
			return shouldAttack;
		}

		return false;
	}


	void LateUpdate(){
		shouldJump = shouldAttack = false;
	}

	public void Click(InputSpace space){
		if (space == InputSpace.Left) {
			shouldJump = true;
		} else if (space == InputSpace.Right) {
			shouldAttack = true;
		}
	}

	public void ClickLeft(){
		Click (InputSpace.Left);
	}

	public void ClickRight(){
		Click (InputSpace.Right);
	}
}
