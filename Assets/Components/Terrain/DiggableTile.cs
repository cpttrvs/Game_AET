using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggableTile : Tile
{
    [SerializeField]
    private GameObject rocks = null;
    [SerializeField]
    private GameObject crater = null;
    [SerializeField]
    private GameObject ground = null;

    [SerializeField]
    private Item burriedItem = null;

    [SerializeField]
    private bool isDigged = false;
    [SerializeField]
    private bool isFilled = false;

    private void Start()
    {
        rocks.SetActive(!isDigged && !isFilled);
        ground.SetActive(isFilled);
        crater.SetActive(isDigged || isFilled);

        if(burriedItem != null)
        {
            burriedItem.gameObject.SetActive(isDigged && !isFilled);
        }

        burriedItem.OnGrab += Item_OnGrab;
    }

    public void Dig()
    {
        if (isDigged) return;

        rocks.SetActive(false);
        ground.SetActive(false);
        crater.SetActive(true);

        if(burriedItem != null)
            burriedItem.gameObject.SetActive(true);

        isDigged = true;
        isFilled = false;
    }

    public void Fill()
    {
        if (isFilled) return;

        rocks.SetActive(false);
        ground.SetActive(true);
        crater.SetActive(true);

        if (burriedItem != null)
            burriedItem.gameObject.SetActive(false);

        isFilled = true;
        isDigged = false;
    }

    private void Item_OnGrab(Item i)
    {
        if(burriedItem.gameObject.activeInHierarchy)
        {
            RemoveItem();
        }
    }

    public Item RemoveItem()
    {
        Item i = burriedItem;
        i.OnGrab -= Item_OnGrab;

        burriedItem = null;

        return i;
    }
}
