using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DoorTriger : MonoBehaviour
{
    [SerializeField]
    GameObject[] warningLight;
    [SerializeField]
    AudioClip alarmSound;
    [SerializeField]
    AudioMixerGroup audioMixer;
    Animation doorAni;
    [SerializeField]
    BoxCollider a;
    AudioSource audioAlram;
    private void Awake()
    {
        doorAni = GetComponent<Animation>();
        audioAlram =GetComponent<AudioSource>();
    }
    private void Update()
    {
      
    }
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.F))
        {   
            for (int i = 0; i < warningLight.Length; i++)
            {
                var warningAni = warningLight[i].GetComponent<Animation>();
                warningLight[i].transform.Find("Cylinder.104").transform.Find("Spotlight").GetComponent<Light>().enabled = true;
                warningAni.Play();
            }
            audioAlram.Play();
            a.enabled = false;
        }
        
    }
}
