using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField]
    [Header("UI")]
    private GameObject quitButton = null;
    [SerializeField]
    private GameObject quitPanel = null;
    [SerializeField]
    private GameObject helpButton = null;

    [Space]
    [Header("시작 연출")]
    [SerializeField]
    private Animator monsterAnim;
    [SerializeField]
    private Camera mobCam;

    [Space]
    [Header("카메라 회전")]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Vector3 finalPos;
    [SerializeField] private Vector3 finalRot;
    [SerializeField] private float finalFov;


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
        Fade.Instance.FadeIn(1.25f);

        monsterAnim.SetTrigger("Bite");

        StartCoroutine(GameStartCoroutine());
    }

    private IEnumerator GameStartCoroutine()
    {
        float timer = 0f;
        float retTime = Time.deltaTime * rotationSpeed;
        while(timer < 1.3f)
        {
            timer += Time.deltaTime;

            mobCam.transform.position = Vector3.Lerp(mobCam.transform.position, finalPos, retTime);
            mobCam.transform.rotation = Quaternion.Slerp(mobCam.transform.rotation, Quaternion.Euler(finalRot), retTime);
            mobCam.fieldOfView = Mathf.Lerp(mobCam.fieldOfView, finalFov, retTime);
            
            yield return null;
        }
        //yield return new WaitForSeconds(2f);
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
