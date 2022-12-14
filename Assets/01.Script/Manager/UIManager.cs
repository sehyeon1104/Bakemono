using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [Header("QuestUI")]
    public GameObject QuestPanel;
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questContent;

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
            PauseGame();
        }
    }

    public void DisableAllPanels()
    {
        QuestPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    public void PauseGame()
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

    public void ToggleQuestUI(bool toggle)
    {
        QuestPanel.SetActive(toggle);
    }

    public void GameQuit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
