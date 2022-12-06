using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveManager : MonoSingleton<SaveManager>
{
    private string SAVE_PATH = "";
    private string SAVE_FILENAME = "/SaveFile.json";

    [SerializeField] private PlayerBase playerData = null;

    public PlayerBase CurrentUser { get { return playerData; } }

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
        InvokeRepeating("SaveToJson", 1f, 60f);
    }

    private void LoadFromJson()
    {
        if (File.Exists(SAVE_PATH + SAVE_FILENAME))
        {
            string json = File.ReadAllText(SAVE_PATH + SAVE_FILENAME);
            json = Crypto.AESDecrypt128(json);
            playerData = JsonUtility.FromJson<PlayerBase>(json);
        }
    }

    public void SaveToJson()
    {
        string json = JsonUtility.ToJson(playerData, true);
        json = Crypto.AESEncrypt128(json);
        File.WriteAllText(SAVE_PATH + SAVE_FILENAME, json, System.Text.Encoding.UTF8);
    }

    private void OnApplicationQuit()
    {
        SaveToJson();
    }

}