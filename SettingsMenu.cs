using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class SettingsMenu : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;

    const string MIXER_MUSIC = "MusicVolume";
    const string MIXER_SFX = "SFXVolume";

    private void Awake()
    {
    }

    public void SetVolume (float volume)
    {
        AudioListener.volume = volume;
    }

    public void SetMusic (bool toggle)
    {
        if (toggle)
            mixer.SetFloat(MIXER_MUSIC, 0);
        else
            mixer.SetFloat(MIXER_MUSIC, -80);

    }

    public void SetSFX(bool Mtoggle)
    {
        if (Mtoggle)
            mixer.SetFloat(MIXER_SFX, 0);
        else
            mixer.SetFloat(MIXER_SFX, -80);
    }
}
