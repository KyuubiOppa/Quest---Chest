using UnityEngine;
using TMPro;
using DG.Tweening;

public class UI_PlayerMoneyTMP : MonoBehaviour
{
    private GameManager gameManager => GameManager.Instance;
    private TMP_Text moneyText;
    private int currentMoney;
    [Header("Animation เปลี่ยนค่าเงิน")]
    [SerializeField] private Vector3 targetScale;
    private Vector3 originalScale;

    private void Awake()
    {
        moneyText = GetComponent<TMP_Text>();
    }

    void OnApplicationQuit()
    {
        moneyText.transform.DOKill();
    }

    private void OnEnable()
    {
        if (gameManager != null)
            gameManager.OnMoneyChanged += AnimateMoneyText;
    }

    private void OnDisable()
    {
        moneyText.transform.DOKill();
        if (gameManager != null)
            gameManager.OnMoneyChanged -= AnimateMoneyText;
    }

    private void Start()
    {
        originalScale = moneyText.transform.localScale;
        targetScale = originalScale * 1.2f;

        currentMoney = gameManager.playerMoney;
        moneyText.text = currentMoney.ToString();
    }

/// <summary>
/// เด้ง TMP
/// </summary>
    private void AnimateMoneyText()
    {
        int targetMoney = gameManager.playerMoney;
        DOTween.Kill(moneyText);
        moneyText.transform.DOKill(true);
        DOTween.To(
            () => currentMoney,
            x =>
            {
                currentMoney = x;
                moneyText.text = currentMoney.ToString();
            },
            targetMoney,
            0.5f
        )
        .SetEase(Ease.OutQuad)
        .SetTarget(moneyText);
        moneyText.transform
            .DOScale(targetScale, 0.15f)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                moneyText.transform
                    .DOScale(originalScale, 0.15f)
                    .SetEase(Ease.InBack);
            });
    }
}
