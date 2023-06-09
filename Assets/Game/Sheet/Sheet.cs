using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheet{
	public List<Note> notes;
	public float noteTravelTime;
	public AudioClip music;

	public Sheet(List<Note> notes, float noteTravelTime){
		this.notes = notes;
		this.noteTravelTime = noteTravelTime;
	}

	public Sheet(List<Note> notes, float noteTravelTime, AudioClip music){
		this.notes = notes;
		this.noteTravelTime = noteTravelTime;
		this.music = music;
	}
}
