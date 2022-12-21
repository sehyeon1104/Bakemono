using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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

    [Header("GameOver UI")]
    [SerializeField]
    private GameObject gameoverPanel;
    [SerializeField]
    private TextMeshProUGUI diedTMP;

    public GameObject fadeObj = null;

    public bool isPause { private set; get; } = false;

    private void Start()
    {
        fadeObj.SetActive(true);
        Fade.Instance.FadeOut();
        DisableAllPanels();
    }

    private void Update()
    {
        if (Monster.Instance.isDie)
            return;

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

        //if (Input.GetKeyDown(KeyCode.C))
        //{
        //    ToggleGameOverPanel();
        //    SaveManager.Instance.SaveToJson();
        //}
    }
    public void DisableAllPanels()
    {
        questPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameoverPanel.SetActive(false);
        settingPanel.SetActive(false);
        TogglePasswordPanel(false);
        //ToggleSettingPanel();
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

    public void ToggleGameOverPanel()
    {
        DisableAllPanels();
        gameoverPanel.SetActive(!gameoverPanel.activeSelf);
        diedTMP.DOFade(1f, 4f);
        SaveManager.Instance.SaveToJson();
        StartCoroutine(RestartScene());
    }

    private IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(4f);
        Fade.Instance.FadeIn();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }


    public void GameQuit()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }
}