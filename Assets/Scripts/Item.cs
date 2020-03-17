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
        
        this.transform.SetParent(pickedUpBy.InventoryRoot);
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;

        // disable gameobject
        this.gameObject.SetActive(false);
    }

    public virtual void Drop()
    {
        // Drop Mesh
        this.transform.SetParent(null);
        this.transform.position = this.owner.GetDropPosition();
        this.transform.localRotation = Quaternion.identity;
        
        // enable gameobject
        this.gameObject.SetActive(true);
        
        // Release owner
        this.owner = null;
    }
}