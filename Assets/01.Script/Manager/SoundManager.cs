using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoSingleton<SoundManager>
{
    [Header("���ӿ� ���� �Ҹ����� �ҷ����°�")]
   protected AudioSource audiosource;
    [SerializeField]
    protected float pitchRandom = 0.2f;
    protected float basePitch;
    private void Awake()
    {
        audiosource= GetComponent<AudioSource>();
    }
    private void Start()
    {
        basePitch = audiosource.pitch;
    }
    
    protected void PlayRandomPitch(AudioClip audio)
    {
        float randomPitch = Random.Range(-pitchRandom, pitchRandom);
        audiosource.pitch = randomPitch + basePitch;
        PlaySound(audio);
    }
    protected void PlaySound(AudioClip audio)
    {
        audiosource.Stop();
        audiosource.clip = audio;
        audiosource.Play();
    }
}
