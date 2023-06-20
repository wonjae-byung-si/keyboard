using UnityEngine;
using System.Collections.Generic;
using System;

public static class SheetParser{

	[Serializable]
	private class ParsedJson{
		public int bpm;
		public List<ParsedNote> data;
	}
	
	[Serializable]
	private class ParsedNote : IComparable<ParsedNote>{
		public float time;
		public int noteType;
		public int language;
		public int keyCode;

		public int CompareTo(ParsedNote other){
			return time.CompareTo(other.time);
		}
	}

	public static Sheet ParseSheet(string jsonString){
		ParsedJson parsedJson = JsonUtility.FromJson<ParsedJson>(jsonString);

		List<Note> noteList = new List<Note>();

		Debug.Log(parsedJson.bpm);
		Debug.Log(parsedJson.data);

		foreach(ParsedNote i in parsedJson.data){
			float time = i.time;
			Language language = i.language == 0 ? Language.Korean : Language.English;
			KeyCode keyCode = (KeyCode)i.keyCode;
			NoteType noteType = i.noteType == 0 ? NoteType.Timed : NoteType.Untimed;
			noteList.Add(new Note(time, keyCode, Language.Korean, noteType));
		}

		parsedJson.data.Sort();

		Sheet sheet = ScriptableObject.CreateInstance<Sheet>();
		sheet.noteTravelTime = 60f / parsedJson.bpm;
		sheet.notes = noteList;
		return sheet;
	}
}

