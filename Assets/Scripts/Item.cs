using System;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public ItemDefinition itemDefinition = default;
    public int amount = 0;

    [NonSerialized] 
    public Inventory owner = default;

    public virtual void PickUp(Inventory pickedUpBy)
    {
        this.owner = pickedUpBy;
    }

    public virtual void Drop()
    {
        this.owner = null;
    }
}