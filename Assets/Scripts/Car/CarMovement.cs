using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour {

	private float horizontalMovement, verticalMovement, steeringAngle;
	public float maxSteerAngle = 30, motorForce = 50;
	public WheelCollider frontLeftWheel, frontRightWheel, backLeftWheel, backRightWheel;

	public void GetInput(){
		verticalMovement = Input.GetAxis ("Vertical");
		horizontalMovement = Input.GetAxis ("Horizontal");
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
