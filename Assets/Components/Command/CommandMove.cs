using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMove : Command
{
    [SerializeField]
    private Direction direction = Direction.FORWARD;
    [SerializeField]
    private float distance = 0f;

    private Transform droneTransform = null;

    protected override void Initialise()
    {
        droneTransform = drone.transform;
    }

    protected override void Play()
    {
        Rotate();

        droneTransform.Translate(droneTransform.forward * distance);
    }

    private void Rotate()
    {
        Vector3 vecDirection = DirectionUtil.ToVector(direction);

        Vector3 relativePos = vecDirection - droneTransform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        droneTransform.rotation = rotation;
    }

    private void OnDrawGizmos()
    {
        if(droneTransform != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(droneTransform.position, droneTransform.forward * distance);
        }
    }
}
