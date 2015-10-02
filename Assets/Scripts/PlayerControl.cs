using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel=1,sendInterval=0)]
public class PlayerControl : NetworkBehaviour {

	public ParticleSystem leftExhaust;
	public ParticleSystem rightExhaust;
	public ParticleSystem spaceFog;
	public float maxRoll = 5;
	public float maxPitch = 5;
	public GameObject camTarget;

	private Rigidbody body;
	private ConstantForce force;
	private Vector3 forceVector = Vector3.zero;
	private Text speedometer;
	private float wantedThrottle;

	[SyncVar]
	private float throttle;
	[SyncVar]
	private Vector3 position;
	[SyncVar]
	private Vector3 velocity;
	[SyncVar]
	private Quaternion rotation;
	[SyncVar]
	private Vector3 angularVelocity;

	void Start () {
		body = GetComponent<Rigidbody>();
		if (isLocalPlayer) {
			speedometer = (Text) FindObjectOfType (typeof(Text));
			GameObject camera = GameObject.FindGameObjectWithTag ("MainCamera");
			camera.GetComponent<TrackingCamera> ().Target = camTarget.transform;
		} else {
			force = GetComponent<ConstantForce> ();
		}
	}

	[Command(channel=0)]
	void CmdControls(Quaternion rotationDelta, float newThrottle) {
		transform.rotation = transform.rotation * rotationDelta;
		throttle = newThrottle;
		forceVector.x = newThrottle;
		force.relativeForce = forceVector;
	}



	void FixedUpdate() {
		if (leftExhaust) {
			leftExhaust.startSpeed = throttle / 6f;
			leftExhaust.emissionRate = throttle;
		}
		if (rightExhaust) {
			rightExhaust.startSpeed = throttle / 6f;
			rightExhaust.emissionRate = throttle;
		}

		if (isClient) {
			body.transform.position = Vector3.Lerp (body.transform.position, position, 0.1f);
			body.velocity = velocity;
			body.transform.rotation = Quaternion.Lerp(body.transform.rotation, rotation, 0.1f);
			body.angularVelocity = angularVelocity;
		} else {
			position = body.transform.position;
			velocity = body.velocity;
			rotation = body.transform.rotation;
			angularVelocity = body.angularVelocity;
		}

		if (!isLocalPlayer) {
			return;
		}

		spaceFog.emissionRate = body.velocity.magnitude / 2f;
		spaceFog.startLifetime = 50f / body.velocity.magnitude;

		// Keyboard

		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			wantedThrottle = 0;
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			wantedThrottle = 11;
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			wantedThrottle = 22;
		} else if (Input.GetKeyDown (KeyCode.Alpha4)) {
			wantedThrottle = 33;
		} else if (Input.GetKeyDown (KeyCode.Alpha5)) {
			wantedThrottle = 45;
		} else if (Input.GetKeyDown (KeyCode.Alpha6)) {
			wantedThrottle = 56;
		} else if (Input.GetKeyDown (KeyCode.Alpha7)) {
			wantedThrottle = 67;
		} else if (Input.GetKeyDown (KeyCode.Alpha8)) {
			wantedThrottle = 78;
		} else if (Input.GetKeyDown (KeyCode.Alpha9)) {
			wantedThrottle = 89;
		} else if (Input.GetKeyDown (KeyCode.Alpha0)) {
			wantedThrottle = 100;
		}

		Quaternion rotationDelta = Quaternion.identity;
		if (Input.GetKey (KeyCode.S)) {
			rotationDelta = rotationDelta * Quaternion.Euler (0, 0, maxPitch);
		} else if (Input.GetKey (KeyCode.W)) {
			rotationDelta = rotationDelta * Quaternion.Euler (0, 0, -maxPitch);
		}
		if (Input.GetKey (KeyCode.A)) {
			rotationDelta = rotationDelta * Quaternion.Euler (maxRoll, 0, 0);
		} else if (Input.GetKey (KeyCode.D)) {
			rotationDelta = rotationDelta * Quaternion.Euler (-maxRoll, 0, 0);
		}

		// Touch

		foreach (Touch touch in Input.touches) {
			if (touch.phase == TouchPhase.Moved && touch.position.x < Screen.width / 8) {
				wantedThrottle = 100f * ((float)touch.position.y) / ((float)Screen.height);
			}
		}

		// Accelerometer

		string msg = "";

		if (Input.acceleration.magnitude > 0f) {
			if (Mathf.Abs (Input.acceleration.x) > 0.1) {
				rotationDelta = rotationDelta * Quaternion.Euler (-maxRoll * Input.acceleration.x, 0, 0);
			}
			msg = ", " + Input.acceleration.x;
			float adjustedPitch = Input.acceleration.z + 0.5f;
			if (Input.acceleration.z < 0f) {
				adjustedPitch = (Input.acceleration.z + 0.5f) * 2;
			} else {
				adjustedPitch = (Input.acceleration.z + 0.5f) / 1.5f;
			}
			msg = msg + "-" + adjustedPitch;
			if (Mathf.Abs (adjustedPitch) > 0.1) {
				rotationDelta = rotationDelta * Quaternion.Euler (0, 0, maxPitch * adjustedPitch);
			}
		}

		speedometer.text = "" + (int)body.velocity.magnitude + ", " + body.position + msg;

		if (rotationDelta != Quaternion.identity || wantedThrottle != throttle) {
			CmdControls (rotationDelta, wantedThrottle);
		}


	}

}
