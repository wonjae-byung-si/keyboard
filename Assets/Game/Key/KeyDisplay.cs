using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

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
		intensity -= Time.deltaTime / fillDisappearTime;
	}

	public void HitEffect(NoteResult result){
		if(result == NoteResult.Good){
			hitColor = Color.cyan;
		}
		else if(result == NoteResult.Meh){
			hitColor = Color.yellow;
		}
		else if(result == NoteResult.Miss){
			hitColor = Color.red;
		}
		else{
			hitColor = Color.white;
		}
		PulseEffect();
	}

	public void PulseEffect(){
		intensity = 1f;
		ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
		emitParams.startColor = hitColor;
		particleSystem.Emit(particleCount);
	}

	public void HoldEffect(){
		intensity = 1f;
	}


	public void ScatterEffect(){
		IEnumerator ScatterCoroutine(){
			Vector2 start = transform.position;
			Vector2 end = start + Random.insideUnitCircle * 160f;

			float duration = 0.5f;
			float timer = 0;
			float randomRotation = Random.Range(-40f, 40f);
			while(timer <= duration){
				timer += Time.deltaTime;
				float progress = timer / duration;
				transform.position = Vector2.Lerp(start, end, progress);
				transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(0, randomRotation, progress));
				yield return null;
			}
		}
		StartCoroutine(ScatterCoroutine());
	}
}

