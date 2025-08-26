using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIAnimation_UIAlphaFadeLoop : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float minAlpha = 0f;
    [SerializeField] private float maxAlpha = 1f;
    [SerializeField] private float duration = 1f;

    private void Start()
    {
        AnimateAlpha();
    }

    private void OnDisable()
    {
        gameObject.transform.DOKill();
    }

    private void OnApplicationQuit()
    {
        gameObject.transform.DOKill();
    }

    private void AnimateAlpha()
    {
        image.DOFade(maxAlpha, duration)
            .From(minAlpha)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
