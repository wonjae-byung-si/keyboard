public enum NoteResult
{
	Good, Meh, Miss, None
	
}

public static class NoteResultChecker{
	public static bool Counts(this NoteResult noteResult){
		if(noteResult == NoteResult.Good || noteResult == NoteResult.Meh){
			return true;
		}
		return false;
	}
}
