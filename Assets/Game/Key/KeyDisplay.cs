using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class KeyDisplay : MonoBehaviour{
	
	public KeyCode keyCode;
	public SpriteRenderer fill;
	public float fillDisappearTime;
	private new ParticleSystem particleSystem;
	public int particleCount;
	private new Light2D light;
	public SpriteMask spriteMask;
	private Vector2 ratio;
	private SpriteRenderer spriteRenderer;
	private Color hitColor = Color.white;

	private Color color{
		get => spriteRenderer.color;
		set{
			spriteRenderer.color = new Color(value.r, value.g, value.b, 1f);
			fill.color = value;
		}
	}
	

	private float intensity{
		get => mIntensity;
		set{ 
			mIntensity = Mathf.Min(Mathf.Max(value, 0f), 1f);
			if(intensity == 0){
				hitColor = Color.white;
			}
			color = Color.Lerp(Color.white, hitColor, intensity);
			light.intensity = intensity;
			spriteMask.transform.localScale = ratio * (1f - intensity);
		}
	}

	private float mIntensity;

	void Start(){
		particleSystem = GetComponent<ParticleSystem>();
		light = GetComponent<Light2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		ratio = spriteRenderer.size / 16;
		fill.size = spriteRenderer.size;
	}

	void Update(){
		if(Input.GetKey(keyCode)){
			intensity = 1f;
		}
		else{
			intensity -= Time.deltaTime / fillDisappearTime;
		}
		if(Input.GetKeyDown(keyCode)){
			ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
			emitParams.startColor = hitColor;
			particleSystem.Emit(particleCount);
		}
	}

	public void HitEffect(NoteResult result){
		if(result == NoteResult.Good){
			Debug.Log(keyCode.ToString() + " Good!");
			hitColor = Color.cyan;
		}
		else if(result == NoteResult.Meh){
			Debug.Log(keyCode.ToString() + " Meh");
			hitColor = Color.yellow;
		}
		else if(result == NoteResult.Miss){
			Debug.Log(keyCode.ToString() + " Miss");
			hitColor = Color.red;
		}
		else{
			hitColor = Color.white;
		}
	}
}

