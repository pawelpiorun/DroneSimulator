using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMovement : MonoBehaviour {

    float speed = 5.0f;
    float rotateStep = 1.0f;
    Rigidbody rigidbody;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {

        var throttle = Input.GetAxis("Throttle");
        var yaw = Input.GetAxis("Yaw");
        var pitch = Input.GetAxis("Pitch");
        var roll = Input.GetAxis("Roll");

        var horizontalMovement = Input.GetAxis("Horizontal");
        var verticalMovement = Input.GetAxis("Vertical");

        rigidbody.AddForce(0, throttle * speed, 0);

        rigidbody.AddTorque(pitch, yaw, roll, ForceMode.Acceleration);

	}
}
