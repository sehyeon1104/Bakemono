using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public bool doQuest { private set; get; } = false;
    public bool isClear { private set; get; } = false;

    private int spawnResearchersCount = 0;

    private void Start()
    {
        doQuest = false;
        isClear = false;
    }

    void EnterKitchen()
    {
        Debug.Log("Enter Kitchen");
        StartCoroutine(KitchenQuest());
    }

    IEnumerator KitchenQuest()
    {
        while (!isClear)
        {
            if(spawnResearchersCount == 0)
            {
                isClear = true;
                doQuest = false;
            }

            yield return null;
        }

        yield break;
    }

    void EnterLaborator()
    {
        Debug.Log("Enter Laborator");
    }

    void ServerRoomQuest()
    {
        Debug.Log("Interfere Communication");
    }

}
