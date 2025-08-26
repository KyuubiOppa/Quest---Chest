using UnityEngine;
using Cysharp.Threading.Tasks;
public class ClearData : MonoBehaviour
{
    private GameManager gameManager => GameManager.Instance;
    private InventoryManager inventoryManager => InventoryManager.Instance;
    [SerializeField] private Tutorial_Shop tutorial_Shop; 
    private const string TutorialKey = "Tutorial_Shop_Done";
    void Start()
    {
        
    }

    public void ClearAllData()
    {
        gameManager.ResetMoney();
        inventoryManager.ClearInventory();
        PlayerPrefs.DeleteKey(TutorialKey);
        PlayerPrefs.Save();
        tutorial_Shop.TutorialFlowAsync().Forget();
    }
}
