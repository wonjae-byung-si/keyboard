using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class SheetDisplay : MonoBehaviour{
	public Sheet sheet{
		get => mSheet;
		set{
			mSheet = value;
			UpdateSheet();
		}
	}

	private Sheet mSheet;
	
	public bool active{
		get => mActive;
		set{
			mActive = value;
			animator.SetBool("Active", active);
		}
	}
	private bool mActive;
	private Animator animator;

	[Header("Plugs")]
	public Image coverImage;
	public TextMeshProUGUI titleText;
	public TextMeshProUGUI descriptionText;

	[HideInInspector] public int index;

	void Awake(){
		animator = GetComponent<Animator>();
	}


	private void UpdateSheet(){
		titleText.text = sheet.title;
		descriptionText.text = sheet.description;
		coverImage.sprite = sheet.cover;
	}


	public void OnClick(){
		GameObject.FindGameObjectWithTag("Sheet Selector").GetComponent<SheetSelection>().activeSheetIndex = index;
	}
}
