using UnityEngine;

public class UntimedKeyNote : KeyNote{
	public UntimedKeyNote(float time, KeyCode key, Language language = Language.English){
		this.time = time;
		this.key = key;
		this.language = language;
	}


	public override GameNote Spawn(){
		GameNote instantiated = base.Spawn();
		instantiated.GetComponent<SpriteRenderer>().color = Color.red;
		return instantiated;
	}

	protected override NoteResult GetNoteResult(){
		return NoteResult.Good;
	}

	protected override float GetNoteHitCheckTime(){
		return Mathf.Infinity;
	}
	
	public override void OnHit(NoteResult result){
		Debug.Log("WWWWWWWWWWWWW");
		if(result.Counts()){
			manager.castanet.Play();
		}
	}
}
