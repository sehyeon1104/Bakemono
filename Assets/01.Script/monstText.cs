using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class monstText : MonoBehaviour
{
    public Canvas maincanvas;
    public void UIEnabledFalse()
    {
        maincanvas.gameObject.SetActive(false);
    }
    public void UIEnabledTrue()
    {
        maincanvas.gameObject.SetActive(true);
    }
}
