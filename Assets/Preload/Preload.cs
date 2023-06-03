using UnityEngine;

static class Preload{
	public static GameObject gameNotePrefab;
	public static GameObject goodHitEffect;
	public static GameObject mehHitEffect;
	public static GameObject missHitEffect;
	public static KeyNoteAppearance keyNoteAppearance;

	static void LoadResources(){
		gameNotePrefab = Resources.Load<GameObject>("Note/GameNotePrefab");
		goodHitEffect = Resources.Load<GameObject>("HitEffects/GoodHit");
		mehHitEffect = Resources.Load<GameObject>("HitEffects/MehHit");
		missHitEffect = Resources.Load<GameObject>("HitEffects/MissHit");
		keyNoteAppearance = Resources.Load<KeyNoteAppearance>("Note/NoteAppearance/KeyNoteAppearance");
	}

	static Preload(){
		LoadResources();
	}
}
