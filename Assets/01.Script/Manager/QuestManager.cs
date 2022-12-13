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
        UIManager.Instance.questTitle.text = "�Ĵ翡�� �����Ӱ� ���� ������ �԰� �־�?";
        UIManager.Instance.questContent.text = "�Ĵ翡�ִ� ����� ��� ���̼���!";

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
        UIManager.Instance.questTitle.text = "���ο� ��ų";
        UIManager.Instance.questContent.text = "�������� ��Ű�� ����� ��� ���̼���!";

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
        UIManager.Instance.questTitle.text = "�ܺη��� ���� ����";
        UIManager.Instance.questContent.text = "����� �ܺη� ������ ���ϰ� �ֽ��ϴ�. �����ϼ���!";
    }

}
