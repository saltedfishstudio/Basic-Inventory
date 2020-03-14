using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] float maxCapacity = 200;
    
    List<Item> items = new List<Item>();

    void AddItem(Item item, int itemAmount)
    {
        items.Add(item);
    }

    float GetSumWeight()
    {
        return items.Sum(e => e.itemDefinition.weight);
    }

    public float AvailableCapacity
    {
        get { return maxCapacity - GetSumWeight(); }
    }

    public void PickUpItem(ItemDefinition itemDefinition, int itemAmount)
    {
        
    }
}
