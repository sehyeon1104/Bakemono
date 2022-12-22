using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;
    private void Awake()
    {
        helpPanel.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && helpPanel.activeSelf)
        {
            helpPanel.SetActive(false);
        }
    }

    public void SetOn()
    {
        helpPanel.SetActive(true);
    }
    public void Quit()
    {
        helpPanel.SetActive(false);
    }
}
