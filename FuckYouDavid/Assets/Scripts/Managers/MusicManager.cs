using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager reference = null;

    private void Awake()
    {
        reference = this;
    }

    public Slider Slider;

    [SerializeField] public AudioClip[] Songs;

    private int LastClipIndex;
    public AudioSource audioSource => GetComponent<AudioSource>() ?? gameObject.AddComponent<AudioSource>();

    void Update()
    {
        if (audioSource.isPlaying == false)
        {
            print("MusicPlaying");
            audioSource.Play();
        }

        if (Slider == null)
        {
            GameObject volumeSilder = GameObject.Find("MusicSlider");

            Slider = volumeSilder == null ? null : volumeSilder.GetComponent<Slider>();

            if (Slider != null && Slider.gameObject.activeSelf)
                Slider.value = audioSource.volume;
        }
        else
        {
            audioSource.volume = Slider.value;
        }
    }
}
