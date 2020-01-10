﻿using UnityEngine;

public class DroneMovementBase : MonoBehaviour
{
    #region Inspector properties

    [ReadOnly] public float Velocity;
    [ReadOnly] public float AngularVelocity;

    public float MaxVelocity = 20.0f;

    public Transform MotorFL;
    public Transform MotorFR;
    public Transform MotorRR;
    public Transform MotorRL;

    public bool HoldAltitude = true;

    // TODO
    //public bool AutoLevel;

    #endregion

    #region Public properties

    [HideInInspector]
    public float[] InputValues = new float[4];

    #endregion

    #region Private properties

    Rigidbody drone;
    Vector3[] motorsForces;
    Transform[] motors;
    Vector3 yawTorque;

    float throttleForce;
    float yawForce;
    float pitchForce;
    float rollForce;

    float? targetHeight;

    #endregion

    void Awake()
    {
        drone = GetComponent<Rigidbody>();
        motorsForces = new Vector3[4];
        motors = new Transform[4]
        {
            MotorFL,
            MotorFR,
            MotorRR,
            MotorRL
        };
    }

    protected void CalculateVelocity()
    {
        Velocity = drone.velocity.magnitude; ;
        AngularVelocity = drone.angularVelocity.magnitude;
    }

    protected void CalculateDrag()
    {
        drone.drag = (Velocity / MaxVelocity) * MaxDrag;
    }

    protected void CalculateMotorsForces()
    {
        var throttle = Input.GetAxis("Throttle");
        var yaw = Input.GetAxis("Yaw");
        var pitch = Input.GetAxis("Pitch");
        var roll = Input.GetAxis("Roll");

        CalculateThrottleForce(throttle);
        yawForce = yaw * MaxYaw;
        pitchForce = pitch * MaxPitch;
        rollForce = roll * MaxRoll;

        ValidateInput();
        SaveInputValues();

        var droneUpTransform = drone.transform.up;
        motorsForces[0] = droneUpTransform * (throttleForce - pitchForce + rollForce);
        motorsForces[1] = droneUpTransform * (throttleForce - pitchForce - rollForce);
        motorsForces[2] = droneUpTransform * (throttleForce + pitchForce - rollForce);
        motorsForces[3] = droneUpTransform * (throttleForce + pitchForce + rollForce);

        // TODO: Simulate yaw with motors, not torque
        yawTorque = droneUpTransform * yawForce;
    }

    protected void CalculateThrottleForce(float throttleInput)
    {
        if (HoldAltitude)
        {
            throttleForce += throttleInput;
            if (throttleInput == 0)
            {
                if (targetHeight == null)
                    targetHeight = drone.transform.position.y;

                // TODO: Hold altitude using PIDController

            }
            else
            {
                targetHeight = null;
            }
        }
        else
            throttleForce = throttleInput * MaxThrottle;
    }

    protected void ValidateInput()
    {
        if (throttleForce > MaxThrottle)
            throttleForce = MaxThrottle;

        if (throttleForce < 0)
            throttleForce = 0;
    }

    protected void SaveInputValues()
    {
        InputValues[0] = throttleForce;
        InputValues[1] = yawForce;
        InputValues[2] = pitchForce;
        InputValues[3] = rollForce;
    }

    protected void ApplyMotorsForces()
    {
        for (int i = 0; i < 4; i++)
        {
            drone.AddForceAtPosition(motorsForces[i], motors[i].transform.position, ForceMode.Force);
        }

        drone.AddTorque(yawTorque, ForceMode.Force);
    }

    public static float MaxThrottle = 10;
    protected const float MaxPitch = 1;
    protected const float MaxRoll = 1;
    protected const float MaxYaw = 1;
    protected const float MaxDrag = 2.0f;
}
