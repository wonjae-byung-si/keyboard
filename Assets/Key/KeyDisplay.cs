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
	

	private float fillOpacity{
		get => fill.color.a;
		set{
			value = Mathf.Min(Mathf.Max(value, 0f), 1f);
			fill.color = new Color(1f, 1f, 1f, value);
			light.intensity = fill.color.a;
			spriteMask.transform.localScale = ratio * (1f - fill.color.a);
		}
	}

	void Start(){
		particleSystem = GetComponent<ParticleSystem>();
		light = GetComponent<Light2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		ratio = spriteRenderer.size / 16;
		fill.size = spriteRenderer.size;
	}

	void Update(){
		if(Input.GetKey(keyCode)){
			fillOpacity = 1f;
		}
		else{
			fillOpacity -= Time.deltaTime / fillDisappearTime;
		}
		if(Input.GetKeyDown(keyCode)){
			particleSystem.Emit(particleCount);
		}
	}
}

