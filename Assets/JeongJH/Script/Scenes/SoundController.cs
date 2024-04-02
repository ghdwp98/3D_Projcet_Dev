using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [SerializeField] AudioMixer masterMixer;
    public Slider audioSlider;

    public void AudioControl()
    {
        float sound = audioSlider.value;

        if (sound == -40f)
        {
            masterMixer.SetFloat("BGM", -80);  //음소거 효과
        }
        else
        {
            masterMixer.SetFloat("BGM", sound);
        }

    }
    public void ToggleAudioVolume() //사운드 토글 소리 키기 끄기 
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //esc로 popup끄기
        {
            Manager.UI.ClosePopUpUI();
        }
    }

}
