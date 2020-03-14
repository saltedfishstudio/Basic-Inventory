using System;
using System.IO;
using UnityEngine;

public class ItemFactory : MonoBehaviour
{
	[Header("Item Setting")]
	public ItemDefinition itemDefinition;
	public int amount = 1;
	public float triggerRadius = 2f;
	public bool autoPickup = false;

	[Header("Activation Setting")] 
	public bool autoActivate = true;
	public EActivateTime activateTime = EActivateTime.Awake;

	Inventory currentInventory = default;
	bool isDeprecated = false;
	
	void Awake()
	{
		if (!autoActivate || activateTime != EActivateTime.Awake) return;

		Activate();
	}

	void Start()
	{
		if (!autoActivate || activateTime != EActivateTime.Start) return;

		Activate();
	}

	void Activate()
	{
		var trigger = GetComponent<SphereCollider>();
		if (trigger == null)
		{
			trigger = gameObject.AddComponent<SphereCollider>();
		}

		trigger.isTrigger = true;
		trigger.radius = triggerRadius;

		// Create Item Mesh
		var itemPrefab = itemDefinition.prefab;
		Item itemInstance = Instantiate(itemPrefab, transform, false);

		itemInstance.transform.localPosition = Vector3.zero;
		itemInstance.transform.localRotation = Quaternion.identity;
		itemInstance.transform.localScale = Vector3.one;
	}


	protected void OnTriggerEnter(Collider other)
	{
		var inventory = other.GetComponent<Inventory>();
		if (inventory == null) return;

		currentInventory = inventory;
		if (autoPickup)
		{
			TryPickUpItem(inventory, amount);
		}
		else
		{
			EventDispatcher.Execute<string>("OnItemFactoryTriggerEnter", $"Press F to pick up {itemDefinition.itemName}.");
		}
	}

	protected void OnTriggerExit(Collider other)
	{
		if (currentInventory == null) return;
		var inventory = other.GetComponent<Inventory>();
		if (inventory == null) return;

		currentInventory = null;
		EventDispatcher.Execute("OnItemFactoryTriggerExit");
	}

	public void TryPickUpItem(Inventory inventory, int itemAmount)
	{
		if (isDeprecated)
		{
			throw new Exception("Already deprecated. this will be destroyed by unity garbage collector.");
		}

		int pickable = GetPickableItemCount(inventory, this.itemDefinition);
		if (pickable > 0)
		{
			itemAmount = itemAmount > amount ? amount : itemAmount;

			int willBePicked = itemAmount >= pickable ? pickable : itemAmount;
			int rest = amount - willBePicked;

			DoPickUp(inventory, willBePicked);

			amount = rest;
			OnPickedUp();
		}
	}

	protected void DoPickUp(Inventory inventory, int itemAmount)
	{
		inventory.PickUpItem(this.itemDefinition, itemAmount);
	}

	protected int GetPickableItemCount(Inventory inventory, ItemDefinition itemDef)
	{
		float available = inventory.AvailableCapacity;
		if (itemDef.weight < 0.0001f)
		{
			return itemDef.maxStack;
		}

		int max = (int) (available / itemDef.weight);

		return itemDef.maxStack > max ? max : itemDef.maxStack;
	}

	protected void OnPickedUp()
	{
		// Notify to UI
		// Play audio clip

		if (amount < 1)
		{
			Deprecate();
		}
	}

	protected void Deprecate()
	{
		isDeprecated = true;
		itemDefinition = null;
		currentInventory = null;

		Destroy(gameObject);
	}

	#region EDITOR
	
#if UNITY_EDITOR
	void OnDrawGizmos()
	{
		DrawGizmos();
	}

	void OnDrawGizmosSelected()
	{
		DrawGizmos();
	}

	void DrawGizmos()
	{
		string thumbnailName = "DefaultItem";
		
		do
		{
			if (itemDefinition == null)
			{
				// Draw Not Initialized Icon
				break;
			}

			Sprite thumbnail = itemDefinition.thumbnail;
			if (thumbnail == null)
			{
				break;
			}

			thumbnailName = itemDefinition.thumbnail.name;
			string extension = ".png";

			Texture loaded =
				UnityEditor.AssetDatabase.LoadAssetAtPath<Texture>(
					$"Assets/Gizmos/{itemDefinition.thumbnail.name}{extension}");
			if (loaded == null)
			{
				string assetPath = UnityEditor.AssetDatabase.GetAssetPath(itemDefinition.thumbnail);
				extension = Path.GetExtension(assetPath);

				string newPath = $"Assets/Gizmos/{thumbnailName}{extension}";

				UnityEditor.AssetDatabase.CopyAsset(assetPath, newPath);
				UnityEditor.AssetDatabase.ImportAsset(newPath);
				UnityEditor.AssetDatabase.Refresh();
				break;
			}

		} while (false);

		Gizmos.DrawIcon(transform.position, thumbnailName);
	}
#endif
	
#endregion
}
