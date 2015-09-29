using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public ParticleSystem leftExhaust;
	public ParticleSystem rightExhaust;
	public Rigidbody body;

	private float throttle;

	public float Throttle {
		get 
		{
			return throttle;
		}
		set
		{
			throttle = value;
			if (leftExhaust) {
				leftExhaust.startSpeed = value / 6f;
				leftExhaust.emissionRate = value;
			}
			if (rightExhaust) {
				rightExhaust.startSpeed = value / 6f;
				rightExhaust.emissionRate = value;
			}
		}
	}
		
	void Start () {
		body = GetComponent<Rigidbody>();
	}

	void Update() {
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Moved && touch.position.x < Screen.width / 8) {
				Throttle = 100f * ((float)touch.position.y) / ((float)Screen.height);
			}
		}
	}
	
	void FixedUpdate () {
	
	}
}
