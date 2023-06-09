using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HitAudio : MonoBehaviour{
	public AudioClip[] audioClip;
	public float pitchMin;
	public float pitchMax;
	private AudioSource audioSource;


	void Start(){
		audioSource = GetComponent<AudioSource>();
	}

	
	public void Play(){
		audioSource.clip = audioClip[Random.Range(0, audioClip.Length)];
		audioSource.pitch = Random.Range(pitchMin, pitchMax);
		audioSource.Play();
	}
}
