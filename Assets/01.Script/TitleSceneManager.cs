using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject quitButton = null;
    [SerializeField]
    private GameObject quitPanel = null;
    [SerializeField]
    private GameObject helpButton = null;

    private bool isQuitPanelActive = false;

    private void Start()
    {
        isQuitPanelActive = false;
        DisableAllPanel();
    }

    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        if (Input.anyKeyDown && !isQuitPanelActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                return;
            }
            GameStart();
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1);
    }

    public void DisableAllPanel()
    {
        quitPanel.SetActive(false);
    }

    public void ToggleQuitPanel()
    {
        quitPanel.SetActive(!quitPanel.activeSelf);
        isQuitPanelActive = quitPanel.activeSelf;
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
