using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    #region Singleton
    public static MusicManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Slider Slider;

    [SerializeField] public AudioClip[] Songs;

    private int LastClipIndex;
    public GameManager GM;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GM = FindObjectOfType<GameManager>();
        audioSource.loop = false;
    }

    void Update()
    {
        if (audioSource.isPlaying == false)
        {
            print("MusicPlaying");
            audioSource.Play();

        }

        if (Slider == null)
        {
            GameObject volumeSilder = GameObject.Find("VolumeSlider");
            Slider = volumeSilder == null ? null : volumeSilder.GetComponent<Slider>();

            if (Slider != null)
                Slider.value = audioSource.volume;
        }
        else
        {
            audioSource.volume = Slider.value;
        }
    }

    public void pickRandomSong()
    {
        if (Songs.Length <= 0)
        {
            Debug.LogError($"No clips in array! {gameObject.name} unable to complete task 'pickRandomSong'");
            return;
        }
        else Debug.Log($"{gameObject.name} Attempting to play new song...");

        int randClip = Random.Range(0, Songs.Length);

        if (randClip == LastClipIndex)
        {
            pickRandomSong();

            Debug.Log($"{gameObject.name} Failure... Retrying.");
        }
        else
        {
            LastClipIndex = randClip;

            audioSource.clip = Songs[randClip];
            audioSource.Play();

            Debug.Log($"{gameObject.name} Success!");
        }
    }

    public void playNextSong()
    {
        if (Songs.Length <= 0)
        {
            Debug.LogError($"No clips in array! {gameObject.name} unable to complete task 'playNextSong'");
            return;
        }
        else Debug.Log($"{gameObject.name} Attempting to play next song...");

        int nextClip = LastClipIndex + 1;

        if (nextClip >= Songs.Length - 1)
        {
            nextClip = 0;
        }

        LastClipIndex = nextClip;

        audioSource.clip = Songs[nextClip];
        audioSource.Play();

        Debug.Log($"{gameObject.name} Success!");
    }

    public void playLastSong()
    {
        if (Songs.Length <= 0)
        {
            Debug.LogError($"No clips in array! {gameObject.name} unable to complete task 'playLastSong'");
            return;
        }
        else Debug.Log($"{gameObject.name} Attempting to play last song...");

        int nextClip = LastClipIndex - 1;

        if (nextClip >= Songs.Length - 1 || nextClip <= 0)
        {
            pickRandomSong();

            Debug.Log($"{gameObject.name} Failure... Playing random.");
        }
        else
        {
            LastClipIndex = nextClip;

            audioSource.clip = Songs[nextClip];
            audioSource.Play();

            Debug.Log($"{gameObject.name} Success!");
        }
    }
}
