using UnityEngine;

public abstract class Note{
	public float time;
	public virtual GameNote Spawn(){
		return InstantiateBaseGameNote();
	}
	public virtual void InitializeGameNote(GameNote gameNote){}

	protected NoteManager manager{
		get{
			if(storedNoteManager == null){
				storedNoteManager = GameObject.FindGameObjectWithTag("Note Manager").GetComponent<NoteManager>();
			}
			return storedNoteManager;
		}
	}

	private static NoteManager storedNoteManager;

	public float timeLeft => manager.time - time;
	public float noteHitCheckTime => GetNoteHitCheckTime();


	public static NoteResult TimeResult(float time){
		if(Mathf.Abs(time) <= 0.1f){
			return NoteResult.Good;
		}
		else if(Mathf.Abs(time) <= 0.25f){
			return NoteResult.Meh;
		}
		else if(time <= 0.8f){
			return NoteResult.Miss;
		}
		return NoteResult.None;
	}

	public abstract NoteHitResult CheckHit();
	

	protected virtual float GetNoteHitCheckTime(){
		return 1f;
	}

	protected GameNote InstantiateBaseGameNote(){
		GameNote instantiated = (Object.Instantiate(Preload.gameNotePrefab) as GameObject).GetComponent<GameNote>();
		instantiated.note = this;
		return instantiated;
	}

	public virtual void OnHit(NoteResult noteResult){}
}
