using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SheetSelection : MonoBehaviour{
	public Sheet[] sheets;
	public GameObject sheetDisplayPrefab;
	private SheetDisplay[] sheetDisplays;
	public float sheetDistance;
	public int activeSheetIndex{
		get => mActiveSheetIndex;
		set{
			if(value < 0 || value >= sheets.Length){
				Debug.LogError("NO NO NO NO NO");
				return;
			}
			moveDistance += activeSheetIndex - value;
			DeactivateSheet();
			mActiveSheetIndex = value;
			ActivateSheet();
			ArrangeSheets(activeSheetIndex);
		}
	}

	[Header("Appearance")]
	public float sheetHeight;
	public float moveSpeed;
	private int mActiveSheetIndex;
	private float moveDistance;

	[Header("Keycodes")]
	[SerializeField] KeyCode leftKeyCode;
	[SerializeField] KeyCode rightKeyCode;
	[SerializeField] KeyCode selectKeyCode;

	void Start(){
		sheetDisplays = new SheetDisplay[sheets.Length];
		for(int i = 0; i < sheets.Length; i++){
			GameObject instantiated = Instantiate(sheetDisplayPrefab, transform);
			sheetDisplays[i] = instantiated.GetComponent<SheetDisplay>();
			sheetDisplays[i].sheet = sheets[i];
			sheetDisplays[i].index = i;
		}

		activeSheetIndex = 0;
		moveDistance = 0;
	}

	void Update(){
		if(Input.GetKeyDown(leftKeyCode) && activeSheetIndex > 0){
			activeSheetIndex--;
		}
		if(Input.GetKeyDown(rightKeyCode) && activeSheetIndex < sheets.Length - 1){
			activeSheetIndex++;
		}
		moveDistance = Mathf.MoveTowards(moveDistance, 0f, moveSpeed * Time.deltaTime);

		ArrangeSheets(activeSheetIndex + moveDistance);

		if(Input.GetKeyDown(selectKeyCode)){
			SelectCurrentSheet();
		}
	}

	private void ArrangeSheets(float progress){
		for(int i = 0; i < sheetDisplays.Length; i++){
			float indexOffset = i - progress;
			sheetDisplays[i].transform.position = new Vector2(sheetDistance * indexOffset, 0);
		}
	}

	private void ActivateSheet(){
		sheetDisplays[activeSheetIndex].active = true;
	}


	private void DeactivateSheet(){
		sheetDisplays[activeSheetIndex].active = false;
	}

	public void SelectCurrentSheet(){
		GlobalData.selectedSheet = sheets[activeSheetIndex];
		SceneManager.LoadScene("Game");
	}
}
