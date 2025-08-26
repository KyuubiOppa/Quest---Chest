using UnityEngine;
using DG.Tweening;

public class UIAnimation_UIFloatingLoop : MonoBehaviour
{
    [SerializeField] private float moveAmount = 50f;
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private UIMoveAxis moveAxis = UIMoveAxis.Y;
    public enum UIMoveAxis
    {
        X,
        Y,
        Z
    }
    private RectTransform rectTransform;
    private Vector3 startPos;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = rectTransform.anchoredPosition;
    }

    private void Start()
    {
        PlayFloating();
    }

    private void OnDisable()
    {
        gameObject.transform.DOKill();
    }

    void OnApplicationQuit()
    {
        gameObject.transform.DOKill();
    }

    private void PlayFloating()
    {
        Vector3 targetPos = startPos;
        switch (moveAxis)
        {
            case UIMoveAxis.X:
                targetPos.x += moveAmount;
                break;
            case UIMoveAxis.Y:
                targetPos.y += moveAmount;
                break;
            case UIMoveAxis.Z:
                targetPos.z += moveAmount;
                break;
        }
        gameObject.transform.DOLocalMove(targetPos, moveDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
