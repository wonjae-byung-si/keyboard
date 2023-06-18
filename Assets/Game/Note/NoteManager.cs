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

	[Header("Combo")]
	public int feverRequirement;
	public int feverMultiplier;
	public TextMeshProUGUI comboText;
	public string comboTextPrefix;
	public string scoreMultiplierPrefix;

	public int combo{
		get => mCombo;
		set{
			mCombo = value;
			UpdateComboText();
		}
	}
	private int mCombo;

	public ComboReward[] comboRewards;

	public int scoreMultiplier{
		get{
			int multiplier = 1;
			foreach(ComboReward i in comboRewards){
				if(i.requirement <= combo){
					multiplier = i.multiplier;
					break;
				}
			}
			return multiplier;
		}
	}

	
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

	private int goodHits;
	private int mehHits;
	private int missHits;

	[Header("Game end")]
	public GameObject gameEndPanel;
	public TextMeshProUGUI gameEndTitleText;
	public TextMeshProUGUI gameEndSummaryText;
	public string clearTitle;
	public string gameoverTitle;

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
			if(!gameEnd && health <= 0){
				Gameover();
			}
			mHealth = Mathf.Min(health, maxHealth);
			UpdateHealthBar();
		}
	}
	private int mHealth;
	private bool gameEnd = false;
	
	[Header("Plugs")]
	public TextMeshProUGUI scoreText;
	public AnimatedFill healthBar;


	void Awake(){
		health = maxHealth;
		keyboard = GameObject.FindGameObjectWithTag("Keyboard").GetComponent<Keyboard>();
		audioSource = GetComponent<AudioSource>();
		enteredNotes = new Queue<GameNote>();

		//InitializeReleaseSheet();
		InitializeDebugSheet();

		time = -sheet.noteTravelTime;

		UpdateScoreText();
		UpdateComboText();

		audioSource.clip = sheet.music;
		if(audioSource.clip != null){
			audioSource.Play();
		}
	}

	private void InitializeDebugSheet(){
		sheet = ScriptableObject.CreateInstance<Sheet>();
		List<Note> noteList = new List<Note>();
		for(int i = 0; i < 1000; i++){
			Note note = new UntimedKeyNote(i * 0.1f, KeyCode.F, Language.Korean);
			noteList.Add(note);
		}
		sheet.noteTravelTime = 1f;
		sheet.notes = noteList;
	}

	private void InitializeReleaseSheet(){
		sheet = GlobalData.selectedSheet;
	}

	void Update(){
		time += Time.deltaTime;
		if(!gameEnd){
			SpawnNotes();
		}
		MoveNotes();
		if(enteredNotes.Count == 0 && noteIndex>= sheet.notes.Count){
			Clear();
		}
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

		if(result == NoteResult.Good){
			goodHits++;
		}
		else if(result == NoteResult.Meh){
			mehHits++;
		}
		else if(result == NoteResult.Miss){
			missHits++;
		}

		if(rewards.TryGetValue(result, out HitReward hitReward)){
			if(hitReward.continueCombo){
				ComboUp();
			}
			else{
				ComboEnd();
			}
			
			score += hitReward.scoreReward * scoreMultiplier;
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
		GameEnd(gameoverTitle);
	}
	
	public void Clear(){
		GameEnd(clearTitle);
	}

	private void GameEnd(string titleString){
		gameEndPanel.SetActive(true);
		keyboard.GetComponent<InputManager>().active = false;
		gameEndTitleText.text = titleString;
		gameEnd = true;
		gameEndSummaryText.text = $"좋음:{goodHits}\n음 이건 좀:{mehHits}\n놓침:{missHits}";
		
	}

	public void ComboUp(){
		combo++;
	}

	public void ComboEnd(){
		combo = 0;
	}

	private void UpdateComboText(){
		comboText.text = $"{comboTextPrefix}{combo.ToString()} ({scoreMultiplierPrefix}{scoreMultiplier})";
	}
}
