using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour{
	public Dictionary<KeyCode, KeyDisplay> keyDisplays;


	void Awake(){
		keyDisplays = new Dictionary<KeyCode, KeyDisplay>();
		foreach(KeyDisplay i in GetComponentsInChildren<KeyDisplay>()){
			keyDisplays.Add(i.keyCode, i);
		}
	}

	public void HitEffect(KeyCode key, NoteResult result){
		keyDisplays[key].HitEffect(result);
	}
}
