using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSound : SoundManager
{
    [SerializeField]
    AudioClip attackAudio,
         gethitAudio, deadAudio,levelUp,biteAudio;

    public void AttackSound()
    {
        PlayRandomPitch(attackAudio);
    }
    public void GetHitAudio()
    {
        PlayRandomPitch(gethitAudio);
    }
    public void DeadSound()
    {
        PlaySound(deadAudio);
    }
    public void audioAdjust()
    {
        PlaySound(attackAudio);
    }
    public void LevelUpSound()
    {
        PlaySound(levelUp);
    }
    public void BiteSound()
    {
        PlaySound(biteAudio);
    }
}
