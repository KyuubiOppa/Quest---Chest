using UnityEngine;
using UnityEngine.UI;

public class Shop_ShowItemPanel : MonoBehaviour
{
    private ShopManager shopManager => ShopManager.Instance;

    [SerializeField] private ItemType itemType;
    [SerializeField] private Image icon;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => shopManager.ShowItemTypes(itemType));
    }

    public ItemType GetItemType()
    {
        return itemType;
    }

    public void SetIconColor(Color color)
    {
        if (icon != null)
            icon.color = color;
    }
}