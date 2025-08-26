using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("Music Audio")]
    [SerializeField] private AudioSource musicAudioSource;
    bool isMuteMusicAudio = false;
    [Header("Effect Audio")]
    [SerializeField] private AudioSource effectAudioSource;
    bool isMuteEffectAudio = false;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (musicAudioSource != null)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = null;
        }
        if (effectAudioSource != null)
        {
            effectAudioSource.Stop();
            effectAudioSource.clip = null;
        }

        if (Instance == this)
            Instance = null;
    }

    private void OnApplicationQuit()
    {
        Instance = null;
    }

    #region Mute Audio
    /// <summary>
    /// Mute เสียงเพลง
    /// </summary>
    public void MuteMusicAudio()
    {
        isMuteMusicAudio = !isMuteMusicAudio;
        musicAudioSource.mute = isMuteMusicAudio;
    }
    /// <summary>
    /// Mute เสียงเอฟเฟค
    /// </summary>
    public void MuteEffectAudio()
    {
        isMuteEffectAudio = !isMuteEffectAudio;
        effectAudioSource.mute = isMuteEffectAudio;
    }

    #endregion

    #region Play Effect Audio

    public void PlayOnShotEffectAudio(AudioClip clip)
    {
        effectAudioSource.PlayOneShot(clip);
    }

#endregion
}
