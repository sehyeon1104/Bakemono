using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.TextCore.Text;

public class PasswordManager : MonoBehaviour
{
    [Header("InputPassword")]
    public Texture faceTexture;
    public TMP_FontAsset fontAsset;
    private string password = "";
    [SerializeField]
    private TextMeshProUGUI[] inputPasswords;
    int pivot = -1;

    [Header("SetPasswordRoom")]
    [SerializeField]
    private List<GameObject> passwordRoom = new List<GameObject>();     // ��ȣ�� ������ �� �ִ� ��� ��
    [SerializeField]
    private GameObject[] passwordRoomLocation;      // ��ȣ�� ������ �� 4��

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
        if(pivot > 2)
        {
            return;
        }
        string n = "";
        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        n = clickObject.GetComponentInChildren<TextMeshProUGUI>().text;

        pivot++;


        inputPasswords[pivot].text = n;
    }

    public void BackPassword()
    {
        if(pivot < 0)
        {
            pivot = 0;
        }

        inputPasswords[pivot].text = "-";
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
            Debug.LogError("��ȣ ��ġ ������ �������� ����");
        }

        // ��ȣ�� ��ġ�� �� �ִ� ��� ���� ����
        for(int i = 0; i < 100; ++i)
        {
            int rand = Random.Range(0, passwordRoom.Count);
            GameObject temp = passwordRoom[rand];
            passwordRoom.Remove(passwordRoom[rand]);
            passwordRoom.Add(temp);
        }

        for(int i = 0; i < passwordRoom.Count; ++i)
        {
            passwordRoom[i].SetActive(false);
        }

        // ��ȣ�� ������ �� ����
        for (int i = 0; i < 4; ++i)
        {
            passwordRoom[i].SetActive(true);
            passwordRoom[i].GetComponentInChildren<TextMeshProUGUI>().text = (i + 1) + password[i].ToString();
            passwordRoom[i].GetComponentInChildren<TextMeshProUGUI>().font = fontAsset;
            passwordRoom[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            //passwordRoom[i].GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetColor("_FaceColor", Color.red);

        }

    }
}
