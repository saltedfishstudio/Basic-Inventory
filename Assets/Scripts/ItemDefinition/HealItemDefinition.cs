using UnityEngine;

[CreateAssetMenu(menuName = "Item Definition/Health Item Definition", order = 400)]
public class HealItemDefinition : ConsumableItemDefinition
{
	public float healAmount = default;
	public float cooldown = default;
	public float channeling = default;
}