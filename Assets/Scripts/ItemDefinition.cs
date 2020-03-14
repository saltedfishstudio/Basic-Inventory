using UnityEngine;

public abstract class ItemDefinition : ScriptableObject
{
    public string itemName = default;
    public Item prefab = default;
    public int maxStack = 1;
    public Sprite thumbnail = default;
    public float weight = 1;
}