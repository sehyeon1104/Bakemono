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
            //File.WriteAllText(SAVE_PATH + SAVE_FILENAME, JsonUtility.ToJson(new User()), System.Text.Encoding.UTF8);
        }
        LoadFromJson();
    }

    private void Start()
    {
        SaveToJson();
        //InvokeRepeating("SaveToJson", 1f, 60f);
    }

    private void LoadFromJson()
    {
        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            json = Crypto.AESDecrypt128(json);
            user = JsonUtility.FromJson<User>(json);
            SceneSerialization.ImportScene(SAVE_SCENE);
        }
    }

    public void SaveToJson()
    {
        string json = JsonUtility.ToJson(user, true);
        json = Crypto.AESEncrypt128(json);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
        SAVE_SCENE = SceneSerialization.SerializeScene(SceneManager.GetActiveScene());
    }

    private void OnApplicationQuit()
    {
        SaveToJson();
    }

}