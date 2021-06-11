using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolumn : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetMasterVolumn(float sliderValue)
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) *20);
    }

    public void SetMusicVolumn(float sliderValue)
    {
        mixer.SetFloat("Music", Mathf.Log10(sliderValue) *20);
    }

    public void SetSfxVolumn(float sliderValue)
    {
        mixer.SetFloat("Sfx", Mathf.Log10(sliderValue) *20);
    }

}
