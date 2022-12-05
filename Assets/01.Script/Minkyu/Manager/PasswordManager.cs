using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PasswordManager : MonoBehaviour
{
    private string password = "";
    [SerializeField]
    private TextMeshProUGUI[] inputPasswords = new TextMeshProUGUI[4];
    int pivot = -1;

    [Header("SetPasswordRoom")]
    [SerializeField]
    private List<GameObject> passwordRoom = new List<GameObject>();     // 암호가 생성될 수 있는 모든 방
    [SerializeField]
    private GameObject[] passwordRoomLocation = new GameObject[4];      // 암호가 생성될 방 4개
    [SerializeField]
    private TextMeshProUGUI[] passwordHint = new TextMeshProUGUI[4];

    private void Awake()
    {
        CreatePassword();
    }

    private void Start()
    {
        SetPasswordRoom();
    }

    void CreatePassword()
    {
        int num = 0;
        string temp = "";
        for(int i = 0; i < 4; ++i)
        {
            num = Random.Range(0, 10);
            temp = num.ToString();
            password += temp;
        }

        Debug.Log("Password : " + password);
    }

    public void InputPassword()
    {
        string n = "";
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        n = clickObject.GetComponentInChildren<TextMeshProUGUI>().text;

        pivot++;

        if(pivot > 3)
        {
            return;
        }

        inputPasswords[pivot].text = n;
    }

    public void BackPassword()
    {
        if(pivot < 0)
        {
            pivot = 0;
        }

        inputPasswords[pivot].text = "0";
        pivot--;
    }

    public void EnterPassword()
    {
        for(int i = 0; i < 4; ++i)
        {
            if(inputPasswords[i].text.ToString() != password[i].ToString())
            {
                Debug.Log("Error!");
                return;
            }
        }

        Debug.Log("Succeed!");
    }

    public void SetPasswordRoom()
    {
        if(passwordRoom.Count <= 0)
        {
            Debug.LogError("암호 배치 구역이 정해지지 않음");
        }

        // 암호가 배치될 수 있는 모든 구역 셔플
        for(int i = 0; i < 100; ++i)
        {
            int rand = Random.Range(0, passwordRoom.Count);
            GameObject temp = passwordRoom[rand];
            passwordRoom.Remove(passwordRoom[rand]);
            passwordRoom.Add(temp);
        }

        // 암호가 생성될 방 설정
        for(int i = 0; i < 4; ++i)
        {
            passwordRoomLocation[i] = passwordRoom[i];
        }

        for(int i = 0; i < 4; ++i)
        {
            passwordHint[i].text = (i + 1) + password[i].ToString();
        }
    }
}
