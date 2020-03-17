using System.Collections.Generic;
using UnityEngine;

namespace SFStudio.OpenWorld.UI
{
    public class InventoryUI : MonoBehaviour
    {
        public Inventory inventory;
        public ItemElement elementPrefab = default;
        
        List<ItemElement> elements = new List<ItemElement>();

        void Awake()
        {
            inventory.onCollectionDirty.AddListener(Repaint);
            elements = new List<ItemElement>();
            
            elementPrefab.gameObject.SetActive(false);
        }

        void Repaint()
        {
            var items = inventory.GetItemList();
            int count = items.Count;

            for (int i = count; i < elements.Count; i++)
            {
                elements[i].gameObject.SetActive(false);
            }
            
            for (int i = 0; i < count; i++)
            {
                var instance = GetElement(i);
                instance.Initialize(items[i]);
                instance.gameObject.SetActive(true);
            }
        }

        ItemElement GetElement(int index)
        {
            if (index < elements.Count)
            {
                return elements[index];
            }

            var instance = Instantiate(elementPrefab, elementPrefab.transform.parent);
            elements.Add(instance);
            
            return instance;
        }
    }
}