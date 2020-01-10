using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrottleTextBehaviour : MonoBehaviour {

    public Text throttleText;
    public Rigidbody drone;

    DroneMovement droneScript;

	// Use this for initialization
	void Start () {
        droneScript = drone.GetComponent<DroneMovement>();
	}

	// Update is called once per frame
	void Update () {
        var currentThrottle = droneScript.InputValues[0];
        throttleText.text = string.Format("{0:0.00}", currentThrottle);
	}
}
