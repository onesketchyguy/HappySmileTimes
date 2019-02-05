using UnityEngine;

public class PlayerPrefsManager
{
    private const string SFX_Volume = "SFX_VOLUME";

    private const string Music_Volume = "MUSIC_VOLUME";

    private const string Master_Volume = "MASTER_VOLUME";

    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(Master_Volume);
    }

    public static void SetMasterVolume(float vol)
    {
        PlayerPrefs.SetFloat(Master_Volume, vol);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(Music_Volume) * GetMasterVolume();
    }

    public static void SetMusicVolume(float vol)
    {
        PlayerPrefs.SetFloat(Music_Volume, vol);
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_Volume) * GetMasterVolume();
    }

    public static void SetSFXVolume(float vol)
    {
        PlayerPrefs.SetFloat(SFX_Volume, vol);
    }
}