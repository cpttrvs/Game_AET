using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandGrab : Command
{
    [SerializeField]
    private float radius = 10f;

    [SerializeField]
    private string rayTag = "Item";

    [SerializeField]
    private float duration = 1f;

    private Inventory droneInventory = null;

    private bool grabbing = false;

    protected override void Initialise()
    {
        droneInventory = drone.inventory;
    }

    protected override IEnumerator Play()
    {
        yield return Grab();

        End();
    }

    private IEnumerator Grab()
    {
        if (!grabbing)
        {
            grabbing = true;

            Collider[] hitColliders = Physics.OverlapSphere(drone.transform.position, radius);
            foreach (Collider c in hitColliders)
            {
                if (c.CompareTag(rayTag))
                {
                    Item item = c.GetComponentInChildren<Item>().Grab();
                    if (item != null)
                    {
                        Debug.Log("GRAB: " + item.name);
                        droneInventory.TryAddItem(item);
                    }
                }
            }

            yield return new WaitForSeconds(duration);

            grabbing = false;
        }
    }
}
