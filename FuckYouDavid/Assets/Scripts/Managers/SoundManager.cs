using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Reference;

    private void Awake()
    {
        Reference = this;
    }

    public AudioClip LevelUp;

    public AudioClip NotificationSound;

    [SerializeField] AudioClip[] DoorSounds;

    private AudioSource m_source;

    private void Start()
    {
        m_source = GetComponent<AudioSource>();
    }

    public void PlayLevelUpEffect()
    {
        if (LevelUp != null)
            m_source.PlayOneShot(LevelUp, 1 /* PlayerPrefsManager.GetSFXVolume()*/);
    }

    public void PlayDoorSound(int soundToPlay)
    {
        m_source.PlayOneShot(DoorSounds[soundToPlay], 1 /* PlayerPrefsManager.GetSFXVolume()*/);
    }

    public void PlayNotificationSound()
    {
        m_source.PlayOneShot(NotificationSound, 1 /* PlayerPrefsManager.GetSFXVolume()*/);
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        m_source.PlayOneShot(clip, volume /* PlayerPrefsManager.GetSFXVolume()*/);
    }
}
