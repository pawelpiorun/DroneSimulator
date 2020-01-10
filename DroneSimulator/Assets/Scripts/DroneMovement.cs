using UnityEngine;

public class DroneMovement : DroneMovementBase
{
    private void Update()
    {
        CalculateVelocity();
        CalculateDrag();
    }

    void FixedUpdate ()
    {
        CalculateMotorsForces();
        ApplyMotorsForces();
    }
}
