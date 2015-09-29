using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float maxTurnRate = 1200f;
	public Vector3 maxImpulse = new Vector3(10f, 10f, 700f);
	public Vector3 velocity = Vector3.zero;
	public float impulseSensitivity = 500f;
	public float turnSensitivity = 1200f;
	public float enginePowerValue = 0f;

	public ParticleSystem leftExhaustFX;
	public ParticleSystem rightExhaustFX;

	private Vector3 impulse = Vector3.zero;
	private float desiredImpulse = 0f;
	private Vector3 impulseActual = Vector3.zero;
	private float maxImpulseChange = 100f;
	private Vector3 turnRate = Vector3.zero;
	private float desiredImpulseInput = 0f;
	private float desiredTurnXInput = 0f;
	private float desiredTurnYInput = 0f;
	private float desiredInputX = 0f;
	private float desiredInputY = 0f; 

	private Transform thisTransform;

	public Vector3 Velocity() {
		get
		{
				return velocity;
		}
	}

	public Vector3 Impulse() {
		get
		{
			return impulse;
		}
		set
		{
			impulse.x = Mathf.Clamp(value.x, 0, maxImpulse.x);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
