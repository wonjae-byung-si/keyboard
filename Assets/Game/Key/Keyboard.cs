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


	public void PulseEffect(KeyCode key){
		keyDisplays[key].PulseEffect();
	}

	public void HitEffect(KeyCode key, NoteResult result){
		keyDisplays[key].HitEffect(result);
	}

	public void ScatterAll(){
		foreach(KeyValuePair<KeyCode, KeyDisplay> i in keyDisplays){
			i.Value.ScatterEffect();
		}
	}

	public void HoldEffect(KeyCode key){
		keyDisplays[key].HoldEffect();
	}
}
