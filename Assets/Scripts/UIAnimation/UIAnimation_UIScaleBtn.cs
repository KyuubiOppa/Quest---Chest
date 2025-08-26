using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIAnimation_UIScaleBtn : MonoBehaviour
{
    private Button button;
    [SerializeField] private Vector3 targetScale = new Vector3(1.1f, 1.1f, 1.1f);
    private Vector3 originalScale;
    private void Awake()
    {
        button = GetComponent<Button>();
        originalScale = button.transform.localScale;
    }

    private void OnEnable()
    {
        button.onClick.AddListener(PlayScaleAnimation);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(PlayScaleAnimation);
    }

    private void PlayScaleAnimation()
    {
        button.transform.DOScale(targetScale, 0.1f).OnComplete(() =>
        {
            button.transform.DOScale(originalScale, 0.1f);
        });
    }
}
