using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private int maxCapacity = 5;

    private List<Item> _items = null;

    public event Action<Item> OnAdd;
    public event Action<Item> OnRemove;

    public bool TryAddItem(Item i)
    {
        if (_items == null) _items = new List<Item>();
        if (_items.Count > maxCapacity)
        {
            Debug.Log("Inventory: try add " + i.name + ", inventory full");
            return false;
        }

        _items.Add(i);
        OnAdd(i);

        Debug.Log("Inventory: added " + i.name + ", capacity " + _items.Count + "/" + maxCapacity);
        return true;
    }

    public bool RemoveItem(Item i)
    {
        if (_items == null) return false;

        if(_items.Contains(i))
        {
            _items.Remove(i);
            OnRemove(i);

            Debug.Log("Inventory: removed " + i.name + ", capacity " + _items.Count + "/" + maxCapacity);
            return true;
        } else
        {
            Debug.Log("Inventory: remove " + i.name + ", not found");
            return false;
        }
    }

    public Item GetItem(int index)
    {
        if (_items == null) return null;
        if (index < 0 || index > _items.Count) return null;

        return _items[index];
    }
}
