using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheet{
	public AudioClip audio;
	public List<Note> notes;
	public float noteTravelTime;

	public Sheet(List<Note> notes, float noteTravelTime){
		this.notes = notes;
		this.noteTravelTime = noteTravelTime;
	}
}
