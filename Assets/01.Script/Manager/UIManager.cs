using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject passwordPanel = null;

    [Header("QuestUI")]
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI questContent;
    [SerializeField]
    private GameObject questPanel;

    [Header("PauseUI")]
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    private GameObject settingPanel;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingPanel.activeSelf)
            {
                ToggleSettingPanel();
            }
            else
            {
                TogglePasswordPanel(false);
                TogglePausePanel();
            }
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleGameOverPanel();
        }
    }
    public void DisableAllPanels()
    {
        questPanel.SetActive(false);
        pausePanel.SetActive(false);
        TogglePasswordPanel(false);
        ToggleSettingPanel();
    }

    public void ToggleQuestUI(bool toggle)
    {
        questPanel.SetActive(toggle);
    }

    public void TogglePasswordPanel(bool toggle)
    {
        passwordPanel.SetActive(toggle);
        MouseManager.Lock(!toggle);
        MouseManager.Visible(toggle);
    }

    public void TogglePausePanel()
    {
        pausePanel.SetActive(!pausePanel.activeSelf);
        settingPanel.SetActive(false);
        isPause = pausePanel.activeSelf;
        MouseManager.Lock(!isPause);
        MouseManager.Visible(isPause);
        if (pausePanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void ToggleSettingPanel()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);
    }

    public void GameQuit()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }
}