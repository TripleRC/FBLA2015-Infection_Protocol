using UnityEngine;
using System.Collections;

/**
 * This script is used to have the camera follows a target
 * In this case, the target is the player
 * It keeps a fixed offset from the target, the same one that 
 * it has when the game starts
 * */
public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 6f;

	Vector3 offset;
	Vector3 offsetOrig;

	void Start()
	{
		if(offsetOrig == null) offsetOrig = target.transform.position - transform.position;
		offset = target.transform.position - transform.position;

		// old code for a camera that doesn't rotate
		// offset = transform.position - target.position;
	}

	void LateUpdate()
	{
		// turn the player based on the mouse's left/right movement
		// float horizontal = Input.GetAxis ("Mouse X") * rotateSpeed;
		// target.transform.Rotate (0, horizontal, 0);

		// allow limit pitching of camera
		offset.y = map (Input.mousePosition.y, 0, Screen.height, offsetOrig.y - 3f, offsetOrig.y + 0.5f);

		// yaw/turn camera based on mouse x/y
		offset.x = map (Input.mousePosition.x, 0, Screen.width, offsetOrig.x - 3f, offsetOrig.x + 3f);

		// get angle of player and get a rotation from it
		// float currentAngle = transform.eulerAngles.y;
		float desiredAngle = target.transform.eulerAngles.y;
		// float angle = Mathf.LerpAngle (currentAngle, desiredAngle, Time.deltaTime * smoothing);
		Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);

		// set your position to the player's position with your offset, 
		// your offset including the rotation that was just calculated
		transform.position = target.transform.position - (rotation * offset);

		// make the camera face the player
		transform.LookAt (target.transform);

		// old code for a camera that doesn't rotate
		// Vector3 targetCamPos = target.position + offset;
		// transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing);
	}

	// maps input from input range to output range
	// e.g. map(0.5, 0, 1, 10, 20) => 15
	public static float map(float input, float inMin, float inMax, float outMin, float outMax)
	{
		return outMin + (outMax - outMin) * ((input - inMin)/(inMax - inMin));
	}
}
