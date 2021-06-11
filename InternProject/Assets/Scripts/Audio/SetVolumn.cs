using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SetVolumn : MonoBehaviour
{

    public AudioMixer mixer;
    [SerializeField] TMP_Text numText;

    public void SetMasterVolumn(float sliderValue)
    {
        mixer.SetFloat("Master", Mathf.Log10(sliderValue) *20);
        numText.text = (sliderValue*100).ToString("F0");
    }

    public void SetMusicVolumn(float sliderValue)
    {
        mixer.SetFloat("Music", Mathf.Log10(sliderValue) *20);
        numText.text = (sliderValue*100).ToString("F0");
    }

    public void SetSfxVolumn(float sliderValue)
    {
        mixer.SetFloat("Sfx", Mathf.Log10(sliderValue) *20);
        numText.text = (sliderValue*100).ToString("F0");
    }

}
