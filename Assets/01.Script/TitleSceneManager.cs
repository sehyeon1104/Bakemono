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
        if (Input.anyKeyDown)
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
        Fade.Instance.FadeIn();
        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(1);
    }

    public void DisableAllPanel()
    {
        quitPanel.SetActive(false);
    }

    public void ToggleQuitPanel()
    {
        Debug.Log("GameStart");
        quitPanel.SetActive(!quitPanel.activeSelf);
    }

    public void GameQuit()
    {
        Application.Quit();
    }
}
