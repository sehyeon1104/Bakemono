using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    AudioMixer AudioMixer;
    [Header("���ӿ� ���� �Ҹ����� �ҷ����°�")]
    [SerializeField] AudioClip monsterAtkAudio;
    public Slider audioSlider;
    public void AudioAdjust()
    {
        audioSlider.value = Mathf.Clamp(audioSlider.value, -35, 0); 
        if(audioSlider.value == -35)
        {
            AudioMixer.SetFloat("MyExposedParam", 40);
        }
        else AudioMixer.SetFloat("MyExposedParam", audioSlider.value);
    }
}
