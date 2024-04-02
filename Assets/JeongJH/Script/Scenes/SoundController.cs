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
            masterMixer.SetFloat("BGM", -80);  //���Ұ� ȿ��
        }
        else
        {
            masterMixer.SetFloat("BGM", sound);
        }

    }
    public void ToggleAudioVolume() //���� ��� �Ҹ� Ű�� ���� 
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //esc�� popup����
        {
            Manager.UI.ClosePopUpUI();
        }
    }

}
