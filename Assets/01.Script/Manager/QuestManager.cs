using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public bool doQuest { private set; get; } = false;
    public bool isClear { private set; get; } = false;

    private void Start()
    {
        doQuest = false;
        isClear = false;
    }

    void EnterKitchen()
    {
        Debug.Log("Enter Kitchen");
    }

    void EnterLaborator()
    {
        Debug.Log("Enter Laborator");
    }

}
