using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour{
	public Sheet sheet;
	[HideInInspector] public float time = 0f;
	int noteIndex = 0;
	public Vector2 startPosition;
	public Vector2 endPosition;
	[HideInInspector] public Keyboard keyboard;
	public float noteDestroyTime;
	
	public Queue<GameNote> enteredNotes;


	void Awake(){
		keyboard = GameObject.FindGameObjectWithTag("Keyboard").GetComponent<Keyboard>();
		enteredNotes = new Queue<GameNote>();

		// Intitialization for Debug
		List<Note> notes = new List<Note>();
		notes.Add(new UntimedKeyNote(1f, KeyCode.A));
		notes.Add(new UntimedKeyNote(1.1f, KeyCode.S));
		notes.Add(new UntimedKeyNote(1.2f, KeyCode.D));
		for(int i = 0; i < 26; i++){
			notes.Add(new TimedKeyNote(2f + 0.2f * i, (KeyCode)((int)KeyCode.A + i)));
		}
		sheet = new Sheet(notes, 3f);

		time = -sheet.noteTravelTime;
	}

	void Update(){
		time += Time.deltaTime;
		SpawnNotes();
		MoveNotes();
		HitNotes();
	}

	private void SpawnNotes(){
		if(noteIndex >= sheet.notes.Count){
			return;
		}
		Note currentNote = sheet.notes[noteIndex];
		if(currentNote.time - sheet.noteTravelTime <= time){
			GameNote spawnedNote = currentNote.Spawn();
			enteredNotes.Enqueue(spawnedNote);
			noteIndex++;
		}
	}

	private void MoveNotes(){
		int destroyCount = 0;
		foreach(GameNote i in enteredNotes){
			float timeLeft = i.note.time - time;
			i.transform.position = NotePosition(timeLeft);
			if(timeLeft < -i.note.decayTime){
				destroyCount++;
			}
		}
		for(int i = 0; i < destroyCount; i++){
			RemoveCurrentNote();
		}
	}

	private Vector2 NotePosition(float timeLeft){
		return Vector2.LerpUnclamped(
				endPosition, startPosition,
				timeLeft / sheet.noteTravelTime);
	}

	private void HitNotes(){
		GameNote currentNote;
		if(enteredNotes.TryPeek(out currentNote)){
			float timeLeft = currentNote.note.time - time;
			if(timeLeft > currentNote.note.noteHitCheckTime){
				return;
			}
			NoteHitResult hitResult = currentNote.note.CheckHit();
			if(hitResult == NoteHitResult.Destroy){
				RemoveCurrentNote();
				return;
			}
		}
	}

	private void RemoveCurrentNote(){
		Destroy(enteredNotes.Peek().gameObject);
		enteredNotes.Dequeue();
	}


	void OnDrawGizmos(){
		Gizmos.DrawLine(startPosition, endPosition);
	}

	public void KeyHit(KeyCode key, NoteResult result){
		Hit(result);
		keyboard.HitEffect(key, result);
	}

	public void Hit(NoteResult result){
		HitEffect(result);
	}

	private void HitEffect(NoteResult result){
		if(result == NoteResult.Good){
			Effect(Preload.goodHitEffect);
		}
		else if(result == NoteResult.Meh){
			Effect(Preload.mehHitEffect);
		}
		else if(result == NoteResult.Miss){
			Effect(Preload.missHitEffect);
		}
	}

	private void Effect(GameObject effect){
		Instantiate(effect).transform.position = endPosition;
	}
}
