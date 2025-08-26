using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    [Header("All Items")]
    [SerializeField] private SO_Item[] allItems;

    [Header("Shop")]
    [SerializeField] private Shop_ItemShopDisplay[] shop_ItemShopDisplays;
    [SerializeField] private Scrollbar shopScrollbar;

    [Header("UI Colors")]
    [SerializeField] private Shop_ShowItemPanel[] itemPanels;
    private Color selectedColor = Color.white;
    private Color unselectedColor = Color.black;
    private ItemType currentSelectedType;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void OnApplicationQuit()
    {
        Instance = null;
    }

    /// <summary>
    /// แสดง Item ตามประเภท
    /// </summary>
    public void ShowItemTypes(ItemType type)
    {
        shopScrollbar.value = 1f;
        currentSelectedType = type;
        UpdateButtonColors();
        var filteredItems = allItems
            .Where(item => item.itemType == type)
            .OrderBy(item =>
            {
                int id;
                return int.TryParse(item.itemID, out id) ? id : int.MaxValue; // ถ้าแปลงไม่ได้ ให้ไปอยู่ท้าย
            })
            .ToArray();
        for (int i = 0; i < shop_ItemShopDisplays.Length; i++)
        {
            if (i < filteredItems.Length)
            {
                shop_ItemShopDisplays[i].gameObject.SetActive(true);
                shop_ItemShopDisplays[i].SetItem(filteredItems[i]);
            }
            else
                shop_ItemShopDisplays[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// อัปเดตสีของปุ่มทั้งหมด
    /// </summary>
    private void UpdateButtonColors()
    {
        foreach (var panel in itemPanels)
        {
            if (panel.GetItemType() == currentSelectedType)
                panel.SetIconColor(selectedColor);
            else
                panel.SetIconColor(unselectedColor);
        }
    }
    
/// <summary>
/// เช็คช่องว่า Item ถูกซื้อหรือยัง
/// </summary>
    public void RefreshOwnedStatus()
    {
        foreach (var display in shop_ItemShopDisplays)
        {
            if (display != null && display.gameObject.activeSelf)
                display.CheckIfItemOwned();
        }
    }
}