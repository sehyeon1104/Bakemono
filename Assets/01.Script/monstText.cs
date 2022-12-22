using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class monstText : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public Canvas maincanvas;
    public void UIEnabledFalse()
    {
        maincanvas.gameObject.SetActive(false);
    }
    public void UIEnabledTrue()
    {
        a.SetActive(false);
        b.SetActive(false);
        maincanvas.gameObject.SetActive(true);
    }
}
