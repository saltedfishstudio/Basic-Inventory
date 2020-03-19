using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace SFStudio.OpenWorld
{

    public class Inventory : MonoBehaviour
    {
        [SerializeField] float maxCapacity = 200;
        public Vector3 dropOffset = Vector3.zero;

        Collection<ItemSlot<Item>, Item> backpack;

        List<Item> items = new List<Item>();
        Transform inventoryRoot;

        public UnityEvent onCollectionDirty = new UnityEvent();

        void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            SetInventoryRoot();
            CreateInventoryCollection();
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

        void CreateInventoryCollection()
        {
            backpack = new Collection<ItemSlot<Item>, Item>.Builder()
                .SetName("Inventory Collection")
                .SetSize(12)
                .Build();
        }

        /// <summary>
        /// BUG
        /// </summary>
        /// <param name="item"></param>
        /// <param name="itemAmount"></param>
        void AddItem(Item item, int itemAmount)
        {
            while (itemAmount > byte.MaxValue)
            {
                var fullItem = Instantiate(item);
                fullItem.amount = byte.MaxValue;
                AddItem(fullItem, byte.MaxValue);

                itemAmount -= byte.MaxValue;
            }

            item.amount = (byte) itemAmount;
            items.Add(item);
        }

        void RemoveItem(Item item, int itemAmount)
        {

            items.Remove(item);
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
            onCollectionDirty.Invoke();
        }

        public void DropItem(Item item, int itemAmount)
        {
            RemoveItem(item, itemAmount);

            item.Drop();
            onCollectionDirty.Invoke();
        }

        public Transform InventoryRoot
        {
            get { return inventoryRoot; }
        }

        public Vector3 GetDropPosition()
        {
            return transform.position + dropOffset;
        }

        public void GrabItem(Item item)
        {
            item.transform.SetParent(inventoryRoot);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }

        public void ReleaseItem(Item item)
        {

        }

        public List<Item> GetItemList()
        {
            return items;
        }
    }
}