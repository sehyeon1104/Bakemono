using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Fade : MonoSingleton<Fade>
{
    [SerializeField] Image fadeImg;

    public void FadeIn(float fadeTime)
    {
        fadeImg.DOFade(1f, fadeTime);
    }

    public void FadeOut(float fadeTime)
    {
        fadeImg.DOFade(0f, fadeTime);
    }
}
