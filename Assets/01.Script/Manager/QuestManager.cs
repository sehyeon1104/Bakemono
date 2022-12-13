using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public bool doQuest { private set; get; } = false;
    public bool isClear { private set; get; } = false;

    public int killedNpcCount = 0;
    

    private void Start()
    {
        doQuest = false;
        isClear = false;
    }

    private void EnterKitchen()
    {
        Debug.Log("Enter Kitchen");
        UIManager.Instance.questTitle.text = "식당에서 여유롭게 아직 음식을 먹고 있어?";
        UIManager.Instance.questContent.text = "식당에있는 사람을 모두 죽이세요!";

        doQuest = true;

        while (!isClear)
        {
            if (killedNpcCount >= 20)
            {
                isClear = true;
                doQuest = false;
            }
            else
            {
                continue;
            }
        }
    }

    void EnterLaborator()
    {
        Debug.Log("Enter Laborator");
        UIManager.Instance.questTitle.text = "새로운 스킬";
        UIManager.Instance.questContent.text = "연구실을 지키는 사람을 모두 죽이세요!";

        doQuest = true;

        while (!isClear)
        {
            if (killedNpcCount >= 20)
            {
                isClear = true;
                doQuest = false;
            }
            else
            {
                continue;
            }
        }

    }

    void ServerRoomQuest()
    {
        Debug.Log("Interfere Communication");
        UIManager.Instance.questTitle.text = "외부로의 교신 방해";
        UIManager.Instance.questContent.text = "사람이 외부로 교신을 전하고 있습니다. 방해하세요!";
    }

}
