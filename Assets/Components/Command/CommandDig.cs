using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandDig : Command
{
    [SerializeField]
    private float radius = 10f;

    [SerializeField]
    private string rayTag = "Diggable";

    [SerializeField]
    private float duration = 1f;

    protected override void Initialise()
    {
    }

    protected override IEnumerator Play()
    {
        Collider[] hitColliders = Physics.OverlapSphere(drone.transform.position, radius);
        foreach (Collider c in hitColliders)
        {
            if (c.CompareTag(rayTag))
            {
                DiggableTile diggable = c.GetComponentInChildren<DiggableTile>();
                if (diggable != null)
                {
                    Debug.Log("DIG: " + diggable.name);
                    diggable.Dig();
                }
            }
        }

        yield return new WaitForSeconds(duration);

        End();
    }
}
