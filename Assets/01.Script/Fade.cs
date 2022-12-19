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

    public void FadeIn()
    {
        fadeImg.DOFade(1f, 2f);
    }

    public void FadeOut()
    {
        fadeImg.DOFade(0f, 2f);
    }
}
