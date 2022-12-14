using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("QuestUI")]
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questContent;
    [SerializeField]
    private GameObject questPanel;


    [Header("PauseUI")]
    [SerializeField]
    private GameObject pausePanel;
    public bool isPause { private set; get; } = false;

    private void Start()
    {
        DisableAllPanels();
    }

    private void Update()
    {
        InputKey();
    }

    private void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePausePanel();
        }
    }

    public void ToggleQuestUI(bool toggle)
    {
        questPanel.SetActive(toggle);
    }

    public void DisableAllPanels()
    {
        questPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void TogglePausePanel()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        isPause = pausePanel.activeSelf;
        MouseManager.Lock(!pausePanel.activeSelf);
        MouseManager.Visible(pausePanel.activeSelf);
        if (pausePanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void GameQuit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}