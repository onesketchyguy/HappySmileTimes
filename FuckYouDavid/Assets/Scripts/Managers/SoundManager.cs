using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public AudioClip LevelUp;

    public AudioClip NotificationSound;

    public AudioClip hit;

    public AudioClip death;

    public AudioClip miss;

    [SerializeField] AudioClip[] DoorSounds;

    private AudioSource m_source;

    private void Start()
    {
        m_source = GetComponent<AudioSource>();
    }

    public void PlayHitSoundEffect()
    {
        if (hit != null)
            m_source.PlayOneShot(hit, 1 /* PlayerPrefsManager.GetSFXVolume()*/);
    }

    public void PlayMissEffect()
    {
        if (miss != null)
            m_source.PlayOneShot(miss, 1 /* PlayerPrefsManager.GetSFXVolume()*/);
    }

    public void PlayDeathSoundEffect()
    {
        if (death != null)
            m_source.PlayOneShot(death, 1 /* PlayerPrefsManager.GetSFXVolume()*/);
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
