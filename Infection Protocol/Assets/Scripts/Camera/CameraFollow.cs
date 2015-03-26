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
	public float smoothing = 5f;

	Vector3 offset;

	void Start()
	{
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing);
	}
}
