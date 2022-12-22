using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.TextCore.Text;
using Unity.VisualScripting;
using UnityEngine.Playables;
public class PasswordManager : MonoSingleton<PasswordManager>
{
    [Header("InputPassword")]
    public Texture faceTexture;
    public TMP_FontAsset fontAsset;
    public PlayableDirector opendoor;
    public PlayableAsset a;
    public string password { private set; get; } = "";
    [SerializeField]
    private TextMeshProUGUI[] inputPasswords;
    int pivot = -1;
    public bool isSucceed { private set; get; } = false;

    [Header("SetPasswordRoom")]
    [SerializeField]
    private List<GameObject> passwordRoom = new List<GameObject>();     // 암호가 생성될 수 있는 모든 방

    private void Start()
    {
      
        if (SaveManager.Instance.CurrentUser.password == "")
        {
            Debug.Log("패스워드 없음");
            CreatePassword();
            SetPasswordRoom();
        }
        else
        {
            Debug.Log("패스워드 있음");
            LoadPasswordRoom();
        }

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
        SaveManager.Instance.CurrentUser.password = password;
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
            if(inputPasswords[i].text.ToString() != SaveManager.Instance.CurrentUser.password[i].ToString())
            {
                Debug.Log("Error!");
                return;
            }
        }
        // 입력한 번호가 맞다면 isSucceed = true
        isSucceed = true;
        opendoor.playableAsset = a;
        opendoor.Play();
        UIManager.Instance.TogglePasswordPanel(false);
        
    }

    public void SetPasswordRoom()
    {
        if(passwordRoom.Count <= 0)
        {
            Debug.LogError("암호 배치 구역이 정해지지 않음");
        }

        // 암호가 배치될 수 있는 모든 구역 셔플
        for (int i = 0; i < 100; ++i)
        {
            int rand = Random.Range(0, passwordRoom.Count);
            GameObject temp = passwordRoom[rand];
            passwordRoom.Remove(passwordRoom[rand]);
            passwordRoom.Add(temp);
        }

        for (int i = 0; i < passwordRoom.Count; ++i)
        {
            passwordRoom[i].SetActive(false);
        }

        // 암호가 생성될 방 설정
        for (int i = 0; i < 4; ++i)
        {
            passwordRoom[i].SetActive(true);
            passwordRoom[i].GetComponentInChildren<TextMeshProUGUI>().text = $"<size=0.5> {(i + 1)}</size>" + SaveManager.Instance.CurrentUser.password[i].ToString();
            passwordRoom[i].GetComponentInChildren<TextMeshProUGUI>().font = fontAsset;
            passwordRoom[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
            //passwordRoom[i].GetComponentInChildren<TextMeshProUGUI>().fontMaterial.SetColor("_FaceColor", Color.red);
            SaveManager.Instance.CurrentUser.passwordRoomsName.Add(passwordRoom[i].name);
        }

    }

    public void LoadPasswordRoom()
    {
        char[] passwordNum = { ' ', };
        int n = 0;
        for(int i = 0; i < passwordRoom.Count; ++i)
        {
            passwordRoom[i].SetActive(false);
        }
        
        foreach(var roomName in SaveManager.Instance.CurrentUser.passwordRoomsName)
        {
            for(int j = 0; j < passwordRoom.Count; ++j)
            {
                if(passwordRoom[j].name == roomName)
                {
                    passwordRoom[j].SetActive(true);
                    passwordRoom[j].GetComponentInChildren<TextMeshProUGUI>().text = $"<size=0.5> {(n + 1)}</size>" + SaveManager.Instance.CurrentUser.password[n].ToString();
                    passwordRoom[j].GetComponentInChildren<TextMeshProUGUI>().color = Color.red;
                    ++n;
                }
            }
        }
    }
}
