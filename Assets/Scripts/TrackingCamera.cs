using UnityEngine;
using System.Collections;

public class TrackingCamera : MonoBehaviour {

	public Transform target;
	public Vector3 distance = new Vector3(0f, 5f, -10f);

	public float positionDamping = 2f;
	public float rotationDamping = 2f;

	private Transform thisTransform;

	void Start () {
		thisTransform = transform;
	}
	
	void LateUpdate () {
		if (!target) {
			return;
		}

		Vector3 wantedPosition = target.position + (target.rotation * distance);
		Vector3 currentPosition = Vector3.Lerp (thisTransform.position, wantedPosition, positionDamping * Time.deltaTime);
		thisTransform.position = currentPosition;

		Quaternion wantedRotation = Quaternion.LookRotation (target.position - thisTransform.position, target.up);
		Quaternion currentRotation = Quaternion.Lerp(thisTransform.rotation, wantedRotation, rotationDamping * Time.deltaTime);
		thisTransform.rotation = currentRotation;
	}
}
