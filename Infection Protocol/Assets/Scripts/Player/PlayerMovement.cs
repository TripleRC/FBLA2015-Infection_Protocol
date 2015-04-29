using UnityEngine;

/**
 * This script handles the player's rotation and movement
 * it makes the player face the ground the mouse is pointed towards
 * and makes the player move when appropriate input is applied
 * */
public class PlayerMovement : MonoBehaviour
{
	public float movementSpeed = 6f;

	Vector3 movement;
	Animator anim;
	Rigidbody playerRigidBody;
	int floorMask;
	float camRayLength = 100f;


	/*
	float maxTurnSpeed = 2.08f; // 3 ^ (1/1.5)
	float minTurnSpeed = 0.01f;
	float turnSpeedPower = 1.5f; // turnSpeeds are raised to this power;

	float lastMouseX = 0;*/
	// Vector3 screenCenter = new Vector3(Screen.width/2, Screen.height/2 + 7, 0); // +7 to look forward

	void Awake()
	{
		floorMask = LayerMask.GetMask ("Floor");
		anim = GetComponent<Animator> ();
		playerRigidBody = GetComponent<Rigidbody> ();
	}

	void FixedUpdate()
	{

		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		h *= 0.2f; // limit left/right movement

		Move (h, v);
		Turning ();
		Animating (h, v);

//		lastMouseX = Input.mousePosition.x;

		/*
		if (Input.GetKey(KeyCode.Escape))
			Screen.lockCursor = false;
		else
			Screen.lockCursor = true;*/
	}

	void Move(float h, float v)
	{
		// get angle of camera and get a rotation from it
		float currentAngle = transform.eulerAngles.y;
		float desiredAngle = Camera.main.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle (currentAngle, desiredAngle, Time.deltaTime * 6f);//smoothing);
		Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);
		
		// set your position to the player's position with your offset, 
		// your offset including the rotation that was just calculated
		// transform.position = target.transform.position - (rotation * offset);
		
		// make the camera face the player
		// transform.LookAt (target.transform);
		
		// old code for a camera that doesn't rotate
		// Vector3 targetCamPos = target.position + offset;
		// transform.position = Vector3.Lerp (transform.position, targetCamPos, smoothing);

		movement.Set (h, 0f, v);
		
		movement = movement.normalized * movementSpeed * Time.deltaTime;

		if(h != 0 && v != 0) transform.rotation = new Quaternion (rotation.x, -rotation.y, rotation.z, rotation.w);;
		transform.Translate (new Vector3(movement.x, 0f, 0f));

		transform.rotation = rotation;
		transform.Translate (new Vector3(0f, 0f, movement.z));
		// playerRigidBody.MovePosition (transform.position + movement);
	}

	void Turning()
	{
		/*
		// turn based on keyboard input
		bool l = Input.GetButton ("Left Turn");
		bool r = Input.GetButton ("Right Turn");

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
		}*/

		/*
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit = ray;
		if (Physics.Raycast (ray, hit)) {
			Vector3 lookTarget = hit.point;

			// rotate towards target
			Vector3 lookDelta = (hit.point-transform.position);
			Quaternion targetRot = Quaternion.LookRotation(lookDelta);
			float rotSpeed = turnSpeed * Time.deltaTime;
			transform.rotation = Quaternion.RotateTowards( transform.rotation, targetRot, rotSpeed );
		}*/

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		RaycastHit floorHit;
		
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;
			
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			
			playerRigidBody.MoveRotation (newRotation);
		}

		/*float currentAngle = transform.eulerAngles.y;
		float desiredAngle = target.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle (currentAngle, desiredAngle, Time.deltaTime * smoothing);
		Quaternion rotation = Quaternion.Euler (0, desiredAngle, 0);

		transform.position = target.transform.position - (rotation * offset);

		Vector3 newrotation = new Vector3 (0, Input.GetAxis ("Mouse X") - lastMouseX, 0);
		transform.Rotate (0, transform.rotation + newrotation, 0);*/

		/** logistic growth turning based on mouse position **/
		/*
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		RaycastHit floorHit;

		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);
			//newRotation.y /= 1f;

			//playerRigidBody.MoveRotation (newRotation);

			*//*
			float screenCenterX = Screen.width/2f;
			float mouseX = Input.mousePosition.x;
			float minDiffFromCenter = 0.5f;

			// if your mouse is >1% from the center of the screen towards the edge
			// set a rotation speed
			if(Mathf.Abs(mouseX - screenCenterX)/(screenCenterX) > minDiffFromCenter)
			{
				float rotSpeed = 0;
				if(mouseX < screenCenterX) // turning left
				{
					float screenLeftMin = 0;
					float screenLeftMax = screenCenterX * (1 - minDiffFromCenter);
					rotSpeed = -map(mouseX, screenLeftMin, screenLeftMax, -maxTurnSpeed, 0);

				} else // turning right
				{
					float screenRightMin = screenCenterX * (1 + minDiffFromCenter);
					float screenRightMax = Screen.width;
					rotSpeed = map(mouseX, screenRightMin, screenRightMax, 0, maxTurnSpeed);
				}
				transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotSpeed);
			}*//*

			float rotSpeed = getRotationSpeed();
			transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, rotSpeed);

		}*/

	}

	void Animating(float h, float v)
	{
		bool walking = h != 0f || v != 0f;
		anim.SetBool ("IsWalking", walking);
	}

	// maps input from input range to output range
	// e.g. map(0.5, 0, 1, 10, 20) => 15
	float map(float input, float inMin, float inMax, float outMin, float outMax)
	{
		return outMin + (outMax - outMin) * ((input - inMin)/(inMax - inMin));
	}

	// returns appropriate rotation speed
	// uses a logistic growth equation with carrying capacity of maxTurnSpeed, raised to a power
	// k value of -8, and 'A' = maxTurnSpeed/minTurnSpeed
	// P        =  M            / (1 + A         				 * e^(-kt)
	// rotSpeed = (maxTurnSpeed / (1 + maxTurnSpeed/minTurnSpeed * e^(-8x)) ^ turnSpeedPower
	// doesn't have to handle left/right negatives because Quaternion.RotateTwords does that
	/*float getRotationSpeed()
	{
		float rotSpeed = 0;
		float kVal = 20f;

		float screenCenterX = Screen.width/2f;
		float mouseX = Input.mousePosition.x;
		float x = Mathf.Abs (mouseX - screenCenterX) / screenCenterX; // between 0 and 1
		rotSpeed = maxTurnSpeed / (1 + maxTurnSpeed/minTurnSpeed * Mathf.Exp(-kVal * x));
		rotSpeed = Mathf.Pow(rotSpeed, turnSpeedPower);

		return rotSpeed;
	}*/
}
