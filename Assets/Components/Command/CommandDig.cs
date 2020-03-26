using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDig : Command
{
    [SerializeField]
    private Direction direction = Direction.FORWARD;
    [SerializeField]
    private float rotationDuration = 1f;
    
    [SerializeField]
    private string rayTag = "Diggable";
    [SerializeField]
    private float diggingDuration = 0.25f;
    [SerializeField]
    private float distance = 10f;

    private bool rotating = false;
    private bool digging = false;
    private Transform droneTransform = null;

    protected override void Initialise()
    {
        droneTransform = drone.transform;
    }

    protected override IEnumerator Play()
    {
        yield return Rotate();

        yield return Dig();

        End();
    }

    private IEnumerator Rotate()
    {
        if (!rotating)
        {
            rotating = true;

            float counter = 0f;
            float rate = 1f / rotationDuration;

            float startAngle = droneTransform.localEulerAngles.y;
            float toAngle = startAngle + DirectionUtil.ToAngle(direction);

            if (startAngle != toAngle)
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
    }

    private IEnumerator Dig()
    {
        if(!digging)
        {
            digging = true;

            RaycastHit[] hits = Physics.RaycastAll(droneTransform.position, droneTransform.forward * distance, distance, LayerMask.NameToLayer(rayTag));

            foreach (RaycastHit h in hits)
            {
                DiggableTile diggableTile = h.collider.GetComponentInChildren<DiggableTile>();

                if (diggableTile != null)
                {
                    diggableTile.Dig();
                }
            }

            yield return new WaitForSeconds(diggingDuration);

            digging = false;
        }
    }
}
