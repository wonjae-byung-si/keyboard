using UnityEngine;
using UnityEditor;

public class SheetParserEditor : EditorWindow{

	string json = "";

	[MenuItem("Window/Sheet Parser Editor")]
	public static void ShowWindow(){
		EditorWindow.GetWindow<SheetParserEditor>("Sheet Parser Editor");
	}

	void OnGUI(){
		GUILayout.Label("Import JSON");
		Debug.Log(json);
		EditorStyles.textField.wordWrap = true;
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("JSON", EditorStyles.boldLabel);
		json = EditorGUILayout.TextArea(json, GUILayout.Height(300));
		EditorGUILayout.EndHorizontal();

		if(GUILayout.Button("Generate")){
			SaveSheet();
		}
	}

	private void SaveSheet(){
		Sheet sheet = SheetParser.ParseSheet(json);
		Debug.Log(json);
		AssetDatabase.CreateAsset(sheet, "Assets/New Sheet.asset");
		AssetDatabase.SaveAssets();
	}
}
