using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSprayEffect : MonoSingleton<BloodSprayEffect>
{
    [SerializeField] private ParticleSystem bloodEffect;
    [HideInInspector] public ParticleSystem BloodEffect;

    private void Awake()
    {
        BloodEffect = Instantiate(bloodEffect);
        BloodEffect.name = "BloodEffect";

        DontDestroyOnLoad(BloodEffect);
    }

}
