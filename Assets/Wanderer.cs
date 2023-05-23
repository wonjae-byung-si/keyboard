using UnityEngine;

public class Wanderer : MonoBehaviour{
	public Vector2 start;
	public Vector2 end;
	public float interval;
	private float timer;
	private Vector2 from;
	private Vector2 to;

	void Update(){
		timer += Time.deltaTime;
		transform.position = Vector2.Lerp(from, to, timer / interval);
		if(timer >= interval){
			from = to;
			to = new Vector2(Random.Range(start.x, end.x), Random.Range(start.y, end.y));
			timer -= interval;
		}
	}
}

