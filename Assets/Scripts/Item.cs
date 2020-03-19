using System;
using System.Collections;
using SFStudio.OpenWorld;
using UnityEngine;

public abstract class Item : MonoBehaviour, IEquatable<Item>, IEquatable<ItemDefinition>
{
    public ItemDefinition itemDefinition = default;
    public byte amount = 0;

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
        if (AssetLoader.initialized)
        {
            DoDrop();
        }
        else
        {
            StartCoroutine(AsyncDrop());
        }
    }

    void DoDrop()
    {
        // Generate Factory
        
        var factory = Instantiate(AssetLoader.itemFactory);
        factory.transform.position = this.owner.GetDropPosition();
        factory.transform.localRotation = Quaternion.identity;
        
        this.transform.SetParent(factory.transform);
        this.transform.localPosition = Vector3.zero;

        var itemFactory = factory.GetComponent<ItemFactory>();
        itemFactory.Activate(this);
        
        // this.transform.SetParent(null);
        // this.transform.position = this.owner.GetDropPosition();
        // this.transform.localRotation = Quaternion.identity;
        
        // enable gameobject
        this.gameObject.SetActive(true);
        
        // Release owner
        this.owner = null;
    }

    IEnumerator AsyncDrop()
    {
        while (!AssetLoader.initialized) yield return null;
        
        DoDrop();
    }

    public bool Equals(Item other)
    {
        throw new NotImplementedException();
    }

    public bool Equals(ItemDefinition other)
    {
        throw new NotImplementedException();
    }

    public override string ToString()
    {
        return $"{itemDefinition.itemName}";
    }
}