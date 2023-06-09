using UnityEngine;

static class Preload{
	public static GameObject gameNotePrefab;
	public static GameObject goodHitEffect;
	public static GameObject mehHitEffect;
	public static GameObject missHitEffect;
	public static KeyNoteAppearance keyNoteAppearanceEnglish;
	public static KeyNoteAppearance keyNoteAppearanceKorean;

	static void LoadResources(){
		gameNotePrefab = Resources.Load<GameObject>("Note/GameNotePrefab");
		goodHitEffect = Resources.Load<GameObject>("HitEffects/GoodHit");
		mehHitEffect = Resources.Load<GameObject>("HitEffects/MehHit");
		missHitEffect = Resources.Load<GameObject>("HitEffects/MissHit");
		keyNoteAppearanceEnglish = Resources.Load<KeyNoteAppearance>("Note/NoteAppearance/KeyNoteAppearanceEnglish");
		keyNoteAppearanceKorean = Resources.Load<KeyNoteAppearance>("Note/NoteAppearance/KeyNoteAppearanceKorean");
	}

	static Preload(){
		LoadResources();
	}
}
