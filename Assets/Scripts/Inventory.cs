using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] float maxCapacity = 200;
    
    List<Item> items = new List<Item>();
    Transform inventoryRoot;

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        SetInventoryRoot();
    }

    void SetInventoryRoot()
    {
        if (inventoryRoot == null)
        {
            inventoryRoot = new GameObject("InventoryRoot").transform;
            inventoryRoot.SetParent(this.transform);
            
            inventoryRoot.localPosition = Vector3.zero;
            inventoryRoot.localRotation = Quaternion.identity;
            inventoryRoot.localScale = Vector3.one;
        }
    }

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

    public void PickUpItem(Item item, int itemAmount)
    {
        AddItem(item, itemAmount);
        
        item.PickUp(this);
    }
}