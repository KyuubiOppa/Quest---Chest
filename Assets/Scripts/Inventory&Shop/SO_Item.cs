using UnityEngine;
[CreateAssetMenu(fileName = "01_ItemType_ItemName", menuName = "Create New Item")]
public class SO_Item : ScriptableObject
{
    [Header("Item Data")]
    public string itemID = "01";
    public string itemName = "ItemName";
    public string itemDescription = "Item Description";
    public Sprite itemSprite;
    [Header("Item Type")]
    public ItemType itemType;
    [Header("Item Price")]
    public int itemPrice = 100;
}
[System.Serializable]
public enum ItemType
{
    Weapon,
    Armor,
    Consumable
}