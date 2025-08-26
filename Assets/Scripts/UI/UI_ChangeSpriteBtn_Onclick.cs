using UnityEngine;
using UnityEngine.UI;
public class UI_ChangeSpriteBtn_Onclick : MonoBehaviour
{
    private bool isChangeImage = false;
    [SerializeField] private Sprite newSprite;
    private Sprite originalSprite;
    [SerializeField] private Image btnImage;

    void OnEnable()
    {
        btnImage = GetComponent<Image>();
        originalSprite = btnImage.sprite;
    }

    public void ChangeSprite()
    {
        if (btnImage != null)
        {
            btnImage.sprite = !isChangeImage ? newSprite : originalSprite;
            isChangeImage = !isChangeImage;
        }
    }
}
