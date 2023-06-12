using UnityEngine;
using UnityEngine.UI;

public class AnimatedFill : MonoBehaviour{
	public float fill;
	public float change;
	public Image image;

	void Update(){
		image.fillAmount = Mathf.MoveTowards(image.fillAmount, fill, change * Time.deltaTime);
	}
}
