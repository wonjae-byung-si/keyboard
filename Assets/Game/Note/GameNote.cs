using UnityEngine;

public class GameNote : MonoBehaviour{
	public Note note{
		get => _note;
		set{
			_note = value;
			_note.InitializeGameNote(this);
		}
	}

	private Note _note;

	public void Decay(){
		Destroy(this);
	}
}
