using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop_ItemShopDisplay : MonoBehaviour
{
    private InventoryManager inventoryManager => InventoryManager.Instance;
    private GameManager gameManager => GameManager.Instance;
    private ShopManager shopManager => ShopManager.Instance;
    public SO_Item item;
    [Header("UI Elements")]
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private TMP_Text itemPriceText;
    [Header("Confirmation Buy")]
    [SerializeField] private Button openBuyConfirmationButton;
    [SerializeField] private Button confirmBuyButton;
    [SerializeField] private Button cancelBuyButton;
    [Header("Owned")]
    [SerializeField] private GameObject ownedPanel;

    void Start()
    {
        openBuyConfirmationButton.onClick.AddListener(OpenConfirmBuyOnClick);
        confirmBuyButton.onClick.AddListener(BuyItem);
        cancelBuyButton.onClick.AddListener(CancelConfirmBuyOnClick);
        CancelConfirmBuyOnClick();
        SetItem(item);
    }

    public void SetItem(SO_Item newItem)
    {
        item = newItem;
        itemImage.sprite = item.itemSprite;
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.itemDescription;
        itemPriceText.text = item.itemPrice.ToString();
        CheckIfItemOwned();
    }

    void OpenConfirmBuyOnClick()
    {
        openBuyConfirmationButton.gameObject.SetActive(false);
        confirmBuyButton.gameObject.SetActive(true);
        cancelBuyButton.gameObject.SetActive(true);
    }

    void CancelConfirmBuyOnClick()
    {
        confirmBuyButton.gameObject.SetActive(false);
        cancelBuyButton.gameObject.SetActive(false);
        openBuyConfirmationButton.gameObject.SetActive(true);
    }

    void BuyItem()
    {
        if (gameManager.playerMoney >= item.itemPrice)
        {
            gameManager.ReduceMoney(item.itemPrice);
            inventoryManager.AddItemToInventory(item);
            openBuyConfirmationButton.gameObject.SetActive(false);
            confirmBuyButton.gameObject.SetActive(false);
            cancelBuyButton.gameObject.SetActive(false);
            shopManager.RefreshOwnedStatus();
        }
        else
        {
            Debug.Log("เงินไม่พอ");
        }
        Debug.Log("ซื้อของ " + item.itemName);
    }

    /// <summary>
    /// ตรวจสอบว่าผู้เล่นมี Item นี้แล้วหรือไม่
    /// </summary>
    public void CheckIfItemOwned()
    {
        if (inventoryManager != null && item != null)
        {
            bool isOwned = inventoryManager.HasItem(item);
            ownedPanel.SetActive(isOwned);
            openBuyConfirmationButton.gameObject.SetActive(!isOwned);
            if (isOwned)
            {
                confirmBuyButton.gameObject.SetActive(false);
                cancelBuyButton.gameObject.SetActive(false);
            }
            else
            {
                CancelConfirmBuyOnClick();
            }
        }
    }
}
