using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteManager : MonoBehaviour{
	private Sheet sheet;

	[HideInInspector] public float time = 0f;

	private int noteIndex = 0;

	[HideInInspector] public Keyboard keyboard;

	[Header("Note")]
	public Vector2 startPosition;
	public Vector2 endPosition;
	public float noteDestroyTime;
	
	public Queue<GameNote> enteredNotes;

	[Header("Hit Reward")]
	public HitReward goodHitReward;
	public HitReward mehHitReward;
	public HitReward missHitReward;

	public int score{
		get => mScore;
		set{
			mScore = value;
			UpdateScoreText();
		}
	}
	private int mScore = 0;

	[Header("Hit Audio")]
	public HitAudio castanet;
	public HitAudio snare;
	private AudioSource audioSource;

	[Header("Health")]
	public int maxHealth;
	public int health{
		get => mHealth;
		set{
			mHealth = value;
			if(mHealth <= 0){
				Gameover();
			}
			UpdateHealthBar();
		}
	}
	private int mHealth;
	
	[Header("Plugs")]
	public TextMeshProUGUI scoreText;
	public AnimatedFill healthBar;


	void Awake(){
		health = maxHealth;
		keyboard = GameObject.FindGameObjectWithTag("Keyboard").GetComponent<Keyboard>();
		audioSource = GetComponent<AudioSource>();
		enteredNotes = new Queue<GameNote>();

		// Intitialization for Debug
		List<Note> notes = new List<Note>();
		notes.Add(new UntimedKeyNote(1f, KeyCode.A));
		notes.Add(new UntimedKeyNote(1.1f, KeyCode.S));
		notes.Add(new UntimedKeyNote(1.2f, KeyCode.D));
		for(int i = 0; i < 100; i++){
			notes.Add(new UntimedKeyNote(2f + 0.1f * i, ((KeyCode)((int)((KeyCode.A + i % 26)))), Language.Korean));
		}
		sheet = new Sheet(notes, 3f);

		time = -sheet.noteTravelTime;

		UpdateScoreText();

		audioSource.clip = sheet.music;
		if(audioSource.clip != null){
			audioSource.Play();
		}
	}

	void Update(){
		time += Time.deltaTime;
		SpawnNotes();
		MoveNotes();
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
			if(timeLeft < -noteDestroyTime){
				destroyCount++;
			}
		}
		for(int i = 0; i < destroyCount; i++){
			Hit(NoteResult.Miss);
			RemoveCurrentNote();
		}
	}

	private Vector2 NotePosition(float timeLeft){
		return Vector2.LerpUnclamped(
				endPosition, startPosition,
				timeLeft / sheet.noteTravelTime);
	}


	public void KeyHit(KeyCode keyCode){
		GameNote currentNote;
		if(enteredNotes.TryPeek(out currentNote)){
			float timeLeft = currentNote.note.time - time;
			NoteHitResult noteHitResult = currentNote.note.CheckHit();
			if(noteHitResult == NoteHitResult.Destroy){
				RemoveCurrentNote();
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
		Dictionary<NoteResult, HitReward> rewards = new Dictionary<NoteResult, HitReward>();
		rewards.Add(NoteResult.Good, goodHitReward);
		rewards.Add(NoteResult.Meh, mehHitReward);
		rewards.Add(NoteResult.Miss, missHitReward);
		if(rewards.TryGetValue(result, out HitReward hitReward)){
			score += hitReward.scoreReward;
			health += hitReward.healthReward;
		}
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

	private void UpdateScoreText(){
		scoreText.text = score.ToString();
	}

	private void UpdateHealthBar(){
		healthBar.fill = (float)health / maxHealth;
	}

	public void Gameover(){
		keyboard.ScatterAll();
		keyboard.GetComponent<InputManager>().active = false;
	}
}	
