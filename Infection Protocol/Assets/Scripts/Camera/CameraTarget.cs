using UnityEngine;
using System.Collections;

public class CameraTarget : MonoBehaviour {

	public Transform target;
	Vector3 offset;
	Vector3 offsetOrig;

	float turnSpeed = 100f;
	
	void Start()
	{
		if(offsetOrig == null) offsetOrig = target.transform.position - transform.position;
		offset = target.transform.position - transform.position;
		
		// old code for a camera that doesn't rotate
		//offset = transform.position - target.position;
	}

	void FixedUpdate () {
		transform.position = target.transform.position - offset;
		//this.transform.position.Set (target.transform.position.x, target.transform.position.y, target.transform.position.z);
		//transform.position.Set(target.position.x, target.position.y, target.position.z);


		// turn based on keyboard input
		bool l = Input.GetButton ("Left Turn");
		bool r = Input.GetButton ("Right Turn");

		// detect mouse l/r
		if(!l && !r)
		{
			float mouseX = Input.mousePosition.x;
			float normX = CameraFollow.map(mouseX, 0, Screen.width, 0, 1);
			if(normX < 0.1) l = true;
			if(normX > 0.9) r = true;
		}

		// if l or r but not both, turn
		if (l ^ r) 
		{
			if(l)
			{
				transform.Rotate(Vector3.down * Time.deltaTime * turnSpeed);
			} else
			{
				transform.Rotate(Vector3.up * Time.deltaTime * turnSpeed);
			}
		}

	}
}
