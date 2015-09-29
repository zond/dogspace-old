using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public ParticleSystem leftExhaust;
	public ParticleSystem rightExhaust;
	public Text speedometer;

	private Rigidbody body;
	private ConstantForce force;
	private Vector3 forceVector = Vector3.zero;
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
			forceVector.x = throttle;
			force.relativeForce = forceVector;
		}
	}
		
	void Start () {
		body = GetComponent<Rigidbody>();
		force = GetComponent<ConstantForce> ();
	}

	void Update() {
		transform.position = body.transform.position;
		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Moved && touch.position.x < Screen.width / 8) {
				Throttle = 100f * ((float)touch.position.y) / ((float)Screen.height);
			}
		}

		speedometer.text = "" + (int) body.velocity.magnitude;

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			Throttle = 10;
		} else if (Input.GetKeyDown(KeyCode.Alpha2)) {
			Throttle = 20;
		} else if (Input.GetKeyDown(KeyCode.Alpha3)) {
			Throttle = 30;
		} else if (Input.GetKeyDown(KeyCode.Alpha4)) {
			Throttle = 40;
		} else if (Input.GetKeyDown(KeyCode.Alpha5)) {
			Throttle = 50;
		} else if (Input.GetKeyDown(KeyCode.Alpha6)) {
			Throttle = 60;
		} else if (Input.GetKeyDown(KeyCode.Alpha7)) {
			Throttle = 70;
		} else if (Input.GetKeyDown(KeyCode.Alpha8)) {
			Throttle = 80;
		} else if (Input.GetKeyDown(KeyCode.Alpha9)) {
			Throttle = 90;
		} else if (Input.GetKeyDown (KeyCode.Alpha0)) {
			Throttle = 100;
		}
	}

}
