using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject item = null;
    [SerializeField]
    private bool isShown = true;

    public event Action<Item> OnGrab;

    public void Show()
    {
        if (isShown) return;
        item.SetActive(true);
        isShown = true;
    }

    public void Hide()
    {
        if (!isShown) return;
        item.SetActive(false);
        isShown = false;
    }

    public Item Grab()
    {
        if (!isShown) Debug.LogWarning("Item: Grab while hidden");

        OnGrab(this);
        return this;
    }
}
