using UnityEngine;

public abstract class KeyNote : Note{
	public KeyCode key;
	public Language language;

	public override GameNote Spawn(){
		GameNote instantiated = base.Spawn();
		Sprite keySprite = Preload.keyNoteAppearance.GetSpriteFromKeyCode(key);
		if(keySprite != null){
			instantiated.GetComponent<SpriteRenderer>().sprite = keySprite;
		}
		return instantiated;
	}



	public override NoteHitResult CheckHit(){
		if(!Input.anyKeyDown){
			return NoteHitResult.None;
		}
		NoteResult result;
		if(Input.GetKeyDown(key)){
			result = GetNoteResult();
		}
		else{ // Wrong key
			result = NoteResult.Miss;
		}
		if(result != NoteResult.None){
			manager.KeyHit(key, result);
			return NoteHitResult.Destroy;
		}
		return NoteHitResult.None;
	}

	protected abstract NoteResult GetNoteResult();
}
