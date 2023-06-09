using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour{
	public Keyboard keyboard;
	public NoteManager noteManager;
	private List<KeyCode> checkKeys;


	void Start(){
		checkKeys = keyboard.keyCodes;
	}

	void Update(){
		foreach(KeyCode i in checkKeys){
			Debug.Log("a");
			if(Input.GetKeyDown(i)){
				noteManager.KeyHit(i);
			}
		}
	}
}
