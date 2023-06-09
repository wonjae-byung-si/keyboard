using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour{
	public Dictionary<KeyCode, KeyDisplay> keyDisplays;
	public List<KeyCode> keyCodes;


	void Awake(){
		keyDisplays = new Dictionary<KeyCode, KeyDisplay>();
		keyCodes = new List<KeyCode>();
		foreach(KeyDisplay i in GetComponentsInChildren<KeyDisplay>()){
			keyDisplays.Add(i.keyCode, i);
			keyCodes.Add(i.keyCode);
		}
	}

	public void HitEffect(KeyCode key, NoteResult result){
		keyDisplays[key].HitEffect(result);
	}
}
