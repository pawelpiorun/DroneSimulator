using UnityEngine;

public class CameraMovementBase : MonoBehaviour
{
    public Transform drone;

    // TODO
    //public bool FPV = false;

    DroneMovement droneScript;
    Transform camera;

    void Start()
    {
        camera = GetComponent<Transform>();
        droneScript = drone.GetComponent<DroneMovement>();
    }

    void Update()
    {
        var throttle = droneScript.InputValues[0];
        var maxThrottle = DroneMovement.MaxThrottle;

        var dronePosition = drone.transform.position;
        var droneYAngle = drone.rotation.y;

        // TODO - animation & rotating
        //var offset = ((throttle / maxThrottle) + 1) * PositionOffset;
        var rotatedOffset = new Vector3
        {
            x = PositionOffset.x * Mathf.Cos(droneYAngle),
            y = PositionOffset.y,
            z = - PositionOffset.x * Mathf.Sin(droneYAngle)
        };
        camera.transform.position = dronePosition + rotatedOffset;

        var yAngle = drone.rotation.eulerAngles.y;
        var rotation = RotationOffest + new Vector3(0, yAngle, 0);
        camera.transform.rotation = Quaternion.Euler(rotation);
    }

    static readonly Vector3 PositionOffset = new Vector3(-1f, 0.6f, 0);
    static readonly Vector3 RotationOffest = new Vector3(15, 90, 0);
}
