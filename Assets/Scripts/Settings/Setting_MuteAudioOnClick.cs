using UnityEngine;

public class Setting_MuteAudioOnClick : MonoBehaviour
{
    [SerializeField] private AudioType audioType;
    private AudioManager audioManager => AudioManager.Instance;

    public enum AudioType
    {
        Music,
        Effect
    }

    public void MuteAudioOnClick()
    {
        switch (audioType)
        {
            case AudioType.Music:
                audioManager.MuteMusicAudio();
                break;
            case AudioType.Effect:
                audioManager.MuteEffectAudio();
                break;
        }
    }
}
