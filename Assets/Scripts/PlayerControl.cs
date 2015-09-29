using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public float maxTurnRate = 1200f;
	public Vector3 maxImpulse = new Vector3(10f, 10f, 700f);
	public Vector3 velocity = Vector3.zero;
	public float impulseSensitivity = 500f;
	public float turnSensitivity = 1200f;

	public ParticleSystem leftExhaustFX;
	public ParticleSystem rightExhaustFX;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
