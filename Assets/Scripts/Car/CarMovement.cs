using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

	private float horizontalMovement, verticalMovement, steeringAngle;
	public float maxSteerAngle = 30, motorForce = 50;
	public WheelCollider frontLeftWheel, frontRightWheel, backLeftWheel, backRightWheel;
	public LevelManager lm;

	private Rigidbody rg;

	void Start(){
		rg = GetComponent<Rigidbody> ();
	}

	public void GetInput(){
		if (!lm.pauseWindow.activeSelf) {
			verticalMovement = Input.GetAxis ("Vertical");
			horizontalMovement = Input.GetAxis ("Horizontal");
		} else {
			rg.velocity = new Vector3 (0f,0f,0f);
			rg.angularVelocity = new Vector3 (0f,0f,0f);
		}

	}

	private void Steer(){
		steeringAngle = maxSteerAngle * horizontalMovement;
		frontLeftWheel.steerAngle = steeringAngle;
		frontRightWheel.steerAngle = steeringAngle;
	}

	private void Accelerate(){
		backLeftWheel.motorTorque = verticalMovement * motorForce;
		backRightWheel.motorTorque = verticalMovement * motorForce;
	}

	void FixedUpdate(){
		GetInput ();
		Steer ();
		Accelerate ();
	} 
}
