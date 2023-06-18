using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Sheet : ScriptableObject{
	public List<Note> notes;
	public float noteTravelTime;
	public AudioClip music;
	public Sprite cover;
	public string title;
	public string description;

}
