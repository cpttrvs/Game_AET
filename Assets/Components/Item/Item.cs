using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public event Action<Item> OnGrab;

    public Item Grab()
    {
        OnGrab(this);
        return this;
    }
}
