using UnityEngine;

class TimedKeyNote : KeyNote{
	public TimedKeyNote(float time, KeyCode key, Language language = Language.English){
		this.time = time;
		this.key = key;
		this.language = language;
	}

	protected override NoteResult GetNoteResult(){
		return TimeResult(timeLeft);
	}

	public override void OnHit(NoteResult noteResult){
		if(noteResult.Counts()){
			manager.snare.Play();
		}
	}
}
