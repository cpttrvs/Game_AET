using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Detector : Technology
{
    [SerializeField]
    private string rayTag = "Item";
    [SerializeField]
    private float durationPerFind = 0.25f;

    private Transform droneTransform = null;

    public event Action<Detector, Item, Coordinate> OnItemFound;

    protected override void Initialise()
    {
        droneTransform = drone.transform;
    }

    protected override IEnumerator Play()
    {
        yield return Detect();

        End();
    }

    private IEnumerator Detect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(droneTransform.position, range);

        int found = 0;

        yield return new WaitForSeconds(duration);

        foreach (Collider c in hitColliders)
        {
            if(c.CompareTag(rayTag))
            {
                Item item = c.gameObject.GetComponentInChildren<Item>();

                if(item != null)
                {
                    found++;

                    Coordinate coordinate = CoordinateUtil.GetCoordinate(droneTransform.position, item.transform.position);

                    Debug.Log("found: " + item.name + " at " + coordinate.ToString());

                    OnItemFound?.Invoke(this, item, coordinate);

                    yield return new WaitForSeconds(durationPerFind);
                }
            }
        }
    }
}
