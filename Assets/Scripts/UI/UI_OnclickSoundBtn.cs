using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class UI_OnclickSoundBtn : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    private Button button;
    private AudioManager audioManager => AudioManager.Instance;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PlaySoundOnClick);
    }

/// <summary>
/// เสียงกด
/// </summary>
    public void PlaySoundOnClick()
    {
        audioManager.PlayOnShotEffectAudio(clickSound);
    }
}
