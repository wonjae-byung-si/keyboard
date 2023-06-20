using UnityEngine;

[System.Serializable]
public class Note{
	public float time;
	public KeyCode key;
	public Language language;
	public NoteType noteType;


	public GameNote Spawn(){
		GameNote instantiated = InstantiateBaseGameNote();
		Sprite keySprite = null;
		if(language == Language.English){
			keySprite = Preload.keyNoteAppearanceEnglish.GetSpriteFromKeyCode(key);
		}
		else if(language == Language.Korean){
			keySprite = Preload.keyNoteAppearanceKorean.GetSpriteFromKeyCode(key);
		}
		if(keySprite != null){
			instantiated.GetComponent<SpriteRenderer>().sprite = keySprite;
		}
		if(noteType == NoteType.Untimed){
			instantiated.GetComponent<SpriteRenderer>().color = Color.red;
		}
		return instantiated;
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

	public NoteHitResult CheckHit(){
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
			OnHit(result);
			return NoteHitResult.Destroy;
		}
		return NoteHitResult.None;
	}

	private NoteResult GetNoteResult(){
		if(noteType == NoteType.Timed){
			return TimeResult(timeLeft);
		}
		else if(noteType == NoteType.Untimed){
			return NoteResult.Good;
		}
		else{
			Debug.LogError("getnoteresult for whatever that notetype is not implemented");
			return NoteResult.None;
		}
	}

	

	private float GetNoteHitCheckTime(){
		if(noteType == NoteType.Untimed){
			return Mathf.Infinity;
		}
		return 1f;
	}

	private GameNote InstantiateBaseGameNote(){
		GameNote instantiated = (Object.Instantiate(Preload.gameNotePrefab) as GameObject).GetComponent<GameNote>();
		instantiated.note = this;
		return instantiated;
	}

	public void OnHit(NoteResult noteResult){
		if(noteResult.Counts()){
			if(noteType == NoteType.Timed){
				manager.snare.Play();
			}
			else if(noteType == NoteType.Untimed){
				manager.castanet.Play();
			}
		}
	}

	public Note(float time, KeyCode keyCode, Language language, NoteType noteType){
		this.time = time;
		this.key = keyCode;
		this.language = language;
		this.noteType = noteType;
	}
}
