using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class Tutorial_Shop : MonoBehaviour
{
    [Header("สอนกดปุ่ม Shop")]
    [SerializeField] private GameObject tutorial_Shop01;
    [SerializeField] private Button menu_ShopButton; // ปุ่มของหน้า Menu 
    [SerializeField] private Button tutorial_ShopButton; // ปุ่มของ Tutorial
    [Header("สอนซื้อของ")]
    [SerializeField] private GameObject tutorial_Shop02;
    [SerializeField] private TMP_Text tutorial_Shop02_Text;
    [SerializeField] private Button buyItemButton;
    [SerializeField] private Button confirmBuyItemButton;

    private const string TutorialKey = "Tutorial_Shop_Done";
    private void Start()
    {
        tutorial_Shop01.SetActive(false);
        tutorial_Shop02.SetActive(false);
        if (PlayerPrefs.GetInt(TutorialKey, 0) == 1)
        {
            return;
        }
        TutorialFlowAsync().Forget();
    }

    /// <summary>
    /// สอนกดปุ่ม Shop
    /// </summary>
    /// <returns></returns>
    public async UniTask TutorialFlowAsync()
    {
        tutorial_Shop01.SetActive(true);

        menu_ShopButton.gameObject.SetActive(false);
        tutorial_ShopButton.gameObject.SetActive(true);
        await tutorial_ShopButton.OnClickAsync(); // รอการกดปุ่ม
        tutorial_ShopButton.gameObject.SetActive(false);
        tutorial_Shop01.SetActive(false);
        menu_ShopButton.gameObject.SetActive(true);
        Tutorial_Shop02FlowAsync().Forget();
    }

    /// <summary>
    /// สอนซื้อของ
    /// </summary>
    /// <returns></returns>
    private async UniTask Tutorial_Shop02FlowAsync()
    {
        await UniTask.Delay(1000);
        tutorial_Shop02.SetActive(true);
        tutorial_Shop02_Text.text = "Press Buy";
        buyItemButton.gameObject.SetActive(true);
        await buyItemButton.OnClickAsync(); // รอการกดปุ่ม
        tutorial_Shop02_Text.text = "Press Yes to purchase the item.";
        await confirmBuyItemButton.OnClickAsync(); // รอการกดปุ่ม
        tutorial_Shop02.SetActive(false);
        PlayerPrefs.SetInt(TutorialKey, 1);
        PlayerPrefs.Save();
    }
}
