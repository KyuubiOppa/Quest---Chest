using Cinemachine;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIStateManager : MonoBehaviour
{
    private ShopManager shopManager => ShopManager.Instance;

    [Header("MainMenu")]
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    private RectTransform mainMenuPanel;

    [Header("Inventory Panel")]
    [SerializeField] private CanvasGroup inventoryCanvasGroup;
    private RectTransform inventoryPanel;
    [SerializeField] private CinemachineVirtualCamera inventoryCamera;
    [Header("Shop Panel")]
    [SerializeField] private CanvasGroup shopCanvasGroup;
    private RectTransform shopPanel;
    [SerializeField] private CinemachineVirtualCamera shopCamera;
    [Header("Clear Data")]
    [SerializeField] private CanvasGroup clearDataCanvasGroup;
    private RectTransform clearDataPanel;

    void Awake()
    {
        InitGetComponent();
        ForceResetAllCanvasGroups();
    }

    void Start()
    {
        ForceResetAllCanvasGroups();
    }

    private void ForceResetAllCanvasGroups()
    {
        // Kill animations ก่อน
        if (mainMenuCanvasGroup != null) 
        {
            mainMenuCanvasGroup.DOKill();
            ResetCanvasGroup(mainMenuCanvasGroup, true);
        }
        
        if (inventoryCanvasGroup != null) 
        {
            inventoryCanvasGroup.DOKill();
            inventoryCamera.gameObject.SetActive(false);
            ResetCanvasGroup(inventoryCanvasGroup, false);
        }
        
        if (shopCanvasGroup != null) 
        {
            shopCanvasGroup.DOKill();
            shopCamera.gameObject.SetActive(false);
            ResetCanvasGroup(shopCanvasGroup, false);
        }
        
        if (clearDataCanvasGroup != null) 
        {
            clearDataCanvasGroup.DOKill();
            ResetCanvasGroup(clearDataCanvasGroup, false);
        }
    }
    private void ResetCanvasGroup(CanvasGroup canvas, bool isActive)
    {
        if (canvas == null) return;
        
        canvas.alpha = isActive ? 1f : 0f;
        canvas.interactable = isActive;
        canvas.blocksRaycasts = isActive;
    }

    private void OnEnable()
    {
        DOTween.KillAll(); 
    }

    private void OnDisable()
    {
        DOTween.KillAll();         
    }

    private void OnDestroy()
    {
        DOTween.KillAll();
    }

    private void OnApplicationQuit()
    {
        DOTween.KillAll();       
    }

    void InitGetComponent()
    {
        mainMenuPanel = mainMenuCanvasGroup.GetComponent<RectTransform>();
        inventoryPanel = inventoryCanvasGroup.GetComponent<RectTransform>();
        shopPanel = shopCanvasGroup.GetComponent<RectTransform>();
        clearDataPanel = clearDataCanvasGroup.GetComponent<RectTransform>();
    }

    public void CanvasGroupActive(CanvasGroup canvas, bool isActive, float duration)
    {
        if (isActive)
        {
            canvas.blocksRaycasts = false;
            canvas.interactable = false;
            canvas.DOFade(1f, duration).SetEase(Ease.Linear).OnComplete(() =>
            {
                canvas.interactable = true;
                canvas.blocksRaycasts = true;
            });
        }
        else
        {
            canvas.interactable = false;
            canvas.blocksRaycasts = false;
            canvas.DOFade(0f, duration).SetEase(Ease.Linear);
        }
    }


    #region Inventory
    public void OpenInventory()
    {
        inventoryCamera.gameObject.SetActive(true);

        CanvasGroupActive(mainMenuCanvasGroup, false, 0.2f);
        CanvasGroupActive(inventoryCanvasGroup, true, 1f);

        // ขยับ Panel
        inventoryPanel.anchoredPosition = new Vector2(961, 0);
        inventoryPanel.DOAnchorPos(new Vector2(0f, 0f), 1f).SetEase(Ease.OutBack)
        .OnComplete(() =>
        {
            inventoryCanvasGroup.blocksRaycasts = true;
        });

    }

    public void CloseInventory()
    {
        inventoryCamera.gameObject.SetActive(false);

        CanvasGroupActive(inventoryCanvasGroup, false, 0.2f);
        CanvasGroupActive(mainMenuCanvasGroup, true, 1f);
    }

    #endregion

    #region Shop

    public void OpenShop()
    {
        shopCamera.gameObject.SetActive(true);

        CanvasGroupActive(mainMenuCanvasGroup, false, 0.2f);
        CanvasGroupActive(shopCanvasGroup, true, 1f);
        
        // ขยับ Panel
        shopPanel.anchoredPosition = new Vector2(961, 0);
        shopPanel.DOAnchorPos(new Vector2(0f, 0f), 1f).SetEase(Ease.OutBack)
        .OnComplete(() =>
        {
            shopCanvasGroup.blocksRaycasts = true;
        });
    }

    public void CloseShop()
    {
        shopCamera.gameObject.SetActive(false);

        CanvasGroupActive(shopCanvasGroup, false, 0.2f);
        CanvasGroupActive(mainMenuCanvasGroup, true, 1f);
    }

    #endregion
    #region Clear Data

    public void OpenClearData()
    {
        CanvasGroupActive(mainMenuCanvasGroup, false, 0.2f);
        CanvasGroupActive(clearDataCanvasGroup, true, 1f);

        // ขยับ Panel
        clearDataPanel.anchoredPosition = new Vector2(961, 0);
        clearDataPanel.DOAnchorPos(new Vector2(0f, 0f), 1f).SetEase(Ease.OutBack);
    }

    public void CloseClearData()
    {
        CanvasGroupActive(clearDataCanvasGroup, false, 0.2f);
        CanvasGroupActive(mainMenuCanvasGroup, true, 1f);
    }

    #endregion
}