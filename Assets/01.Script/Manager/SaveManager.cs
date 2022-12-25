using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.RuntimeSceneSerialization;
using UnityEngine.SceneManagement;

public class SaveManager : MonoSingleton<SaveManager>
{
    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.json";
    private string SAVE_SCENE = "";

    [SerializeField] private User user = null;

    public User CurrentUser { get { return user; } }

    private void Awake()
    {
        SAVE_PATH = Application.dataPath + "/Json";
        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
            File.WriteAllText(SAVE_PATH + SAVE_FILENAME, JsonUtility.ToJson(new User()), System.Text.Encoding.UTF8);
        }
        else
        {
            LoadFromJson();
        }
    }

    private void Start()
    {
        SaveToJson();
        //InvokeRepeating("SaveToJson", 1f, 60f);
    }

    [ContextMenu("LoadFromJson")]
    private void LoadFromJson()
    {
        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            //json = Crypto.AESDecrypt128(json);
            user = JsonUtility.FromJson<User>(json);    
            Debug.Log("로딩완료");

            LoadPlayerStat();
            //SceneSerialization.ImportScene(SAVE_SCENE);
        }
    }

    [ContextMenu("SaveToJson")]
    public void SaveToJson()
    {
        SavePlayerStat();
        //SaveSpawnPos();

        string json = JsonUtility.ToJson(user, true);
        //json = Crypto.AESEncrypt128(json);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
        Debug.Log("저장완료");


        //SAVE_SCENE = SceneSerialization.SerializeScene(SceneManager.GetActiveScene());
    }

    public void SavePlayerStat()
    {
        CurrentUser.maxHp = Monster.Instance.MaxHp;
        CurrentUser.hp = Monster.Instance.CurrentHp;
        CurrentUser.experience = Monster.Instance.CurrentExp;
        CurrentUser.level = Monster.Instance.CurrentLevel;

        if(CurrentUser.hp == 0)
        {
            InitPlayerStat();
        }
    }

    public void InitPlayerStat()
    {
        CurrentUser.hp = 100;
        CurrentUser.maxHp = 100;
        CurrentUser.experience = 0;
    }

    public void LoadPlayerStat()
    {
        Monster.Instance.MaxHp = CurrentUser.maxHp;
        Monster.Instance.CurrentHp = CurrentUser.hp;
        Monster.Instance.CurrentExp = CurrentUser.experience;
        Monster.Instance.CurrentLevel = CurrentUser.level;
    }

    //public void SaveSpawnPos()
    //{
    //    CurrentUser.researcherMaleSpawnPos = GameManager.Instance.researcherMaleSpawnPos;
    //    CurrentUser.researcherFemaleSpawnPos = GameManager.Instance.researcherFemaleSpawnPos;
    //    CurrentUser.soldierSpawnPos = GameManager.Instance.soldierSpawnPos;
    //}

    //public void LoadSpawnPos()
    //{
    //    GameManager.Instance.researcherMaleSpawnPos = CurrentUser.researcherMaleSpawnPos;
    //    GameManager.Instance.researcherFemaleSpawnPos = CurrentUser.researcherFemaleSpawnPos;
    //    GameManager.Instance.soldierSpawnPos = CurrentUser.soldierSpawnPos;
    //}

    //private void OnApplicationQuit()
    //{
    //    SaveToJson();
    //}

}