using UnityEngine;
using UnityEngine.UI;

public class Inventory_InvSlot : MonoBehaviour
{
    public SO_Item item = null;
    [SerializeField] private Image itemImage;

    public void SetItem(SO_Item newItem)
    {
        item = newItem;
        if (itemImage != null && item != null)
        {
            itemImage.enabled = true;
            itemImage.sprite = item.itemSprite;
        }
        else
        {
            if (itemImage != null)
                itemImage.enabled = false;
        }
    }

    public bool HasItem()
    {
        return item != null;
    }

    public void ClearSlot()
    {
        SetItem(null);
    }
}