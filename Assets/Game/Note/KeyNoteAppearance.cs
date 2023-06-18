using UnityEngine;

[CreateAssetMenu]
public class KeyNoteAppearance : ScriptableObject
{
	[SerializeField] private Sprite[] sprite;

	public Sprite GetSpriteFromKeyCode(KeyCode key){
		return sprite[(int)key]; // Unity dictionary serialization support when
	}
}
