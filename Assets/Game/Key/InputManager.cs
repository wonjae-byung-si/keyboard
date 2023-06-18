using UnityEngine;
using System.Collections.Generic;

public class InputManager : MonoBehaviour{
	public Keyboard keyboard;
	public NoteManager noteManager;
	private List<KeyCode> checkKeys;
	[HideInInspector] public bool active = true;


	void Start(){
		checkKeys = keyboard.keyCodes;
	}

	void Update(){
		if(active){
			foreach(KeyCode i in checkKeys){
				if(Input.GetKeyDown(i)){
					noteManager.KeyHit(i);
					keyboard.PulseEffect(i);
				}
				if(Input.GetKey(i)){
					keyboard.HoldEffect(i);
				}
			}
		}
	}
}
