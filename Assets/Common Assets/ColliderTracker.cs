using UnityEngine;

public class ColliderTracker : MonoBehaviour{
	public bool tracked => touchCount > 0;
	public int touchCount = 0;
	public bool trackCollision = true;
	public bool trackTrigger = true;
	
	void OnCollisionEnter(Collision collision){
		OnCollision(1);
	}

	void OnCollisionEnter2D(Collision2D collision){
		OnCollision(1);
	}

	void OnCollisionExit(Collision collision){
		OnCollision(-1);
	}

	void OnCollisionExit2D(Collision2D collision){
		OnCollision(-1);
	}

	void OnTriggerEnter(Collider other){
		OnTrigger(1);
	}

	void OnTriggerEnter2D(Collider2D other){
		OnTrigger(1);
	}

	void OnTriggerExit(Collider other){
		OnTrigger(-1);
	}

	void OnTriggerExit2D(Collider2D other){
		OnTrigger(-1);
	}

	private void OnCollision(int count){
		if(trackCollision){
			touchCount += count;
		}
	}

	private void OnTrigger(int count){
		if(trackTrigger){
			touchCount += count;
		}
	}
}
