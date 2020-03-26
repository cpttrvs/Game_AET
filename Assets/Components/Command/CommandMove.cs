using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMove : Command
{
    [SerializeField]
    private Direction direction = Direction.FORWARD;
    [SerializeField]
    private float distance = 0f;

    [SerializeField]
    private float rotationDuration = 1f;
    [SerializeField]
    private float movementDuration = 1f;

    private Transform droneTransform = null;

    private bool rotating = false;
    private bool moving = false;

    protected override void Initialise()
    {
        droneTransform = drone.transform;

        rotating = false;
        moving = false;
    }

    protected override IEnumerator Play()
    {
        yield return Rotate();

        yield return Move();

        End();
    }

    private IEnumerator Rotate()
    {
        if (rotating) yield return null;

        rotating = true;

        float counter = 0f;
        float rate = 1f / rotationDuration;

        float startAngle = droneTransform.localEulerAngles.y;
        float toAngle = startAngle + DirectionUtil.ToAngle(direction);
        
        if(startAngle != toAngle)
        {
            while (counter < 1f)
            {
                counter += Time.deltaTime * rate;

                counter = Mathf.Clamp(counter, 0f, 1f);
                
                float yLerp = Mathf.LerpAngle(startAngle, toAngle, counter);

                Vector3 vLerp = new Vector3(0, yLerp, 0);
                droneTransform.localRotation = Quaternion.Euler(vLerp);

                yield return null;
            }
        }

        rotating = false;
    }

    private IEnumerator Move()
    {
        if (moving) yield return null;

        moving = true;

        float counter = 0;
        float rate = 1f / movementDuration;

        Vector3 startPos = droneTransform.localPosition;
        Vector3 toPos = startPos + droneTransform.forward * distance;

        while (counter < 1f)
        {
            counter += Time.deltaTime * rate;

            counter = Mathf.Clamp(counter, 0f, 1f);

            droneTransform.localPosition = Vector3.Lerp(startPos, toPos, counter);
            yield return null;
        }

        moving = false;
    }
}
