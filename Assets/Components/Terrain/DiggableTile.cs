﻿using System.Collections;
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

    public override bool IsWalkable() { return isFilled; }

    private void Start()
    {
        rocks.SetActive(!isDigged && !isFilled);
        ground.SetActive(isFilled);
        crater.SetActive(isDigged);

        if(burriedItem != null)
        {
            if (isDigged) burriedItem.Show();
            else burriedItem.Hide();
        }

        burriedItem.OnGrab += Item_OnGrab;
    }

    public void Dig()
    {
        if (isDigged) return;

        rocks.SetActive(false);
        ground.SetActive(false);
        crater.SetActive(true);

        if (burriedItem != null)
            burriedItem.Show();

        isDigged = true;
        isFilled = false;
    }

    public void Fill()
    {
        if (isFilled) return;

        rocks.SetActive(false);
        ground.SetActive(true);

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
