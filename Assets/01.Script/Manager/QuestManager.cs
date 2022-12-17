using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public bool doQuest { private set; get; } = false;
    public bool isClear { private set; get; } = false;

    public int killedNpcCount = 0;
    [SerializeField]
    private GameObject researcherPrefab = null;
    [SerializeField]
    private GameObject soldierPrefab = null;
    [SerializeField]
    private GameObject nursePrefab = null;
    [SerializeField]
    private int summonResearcherCount = 0;
    [SerializeField]
    private int summonSoldierCount = 0;
    [SerializeField]
    private int summonNurseCount = 0;

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
        UIManager.Instance.ToggleQuestUI(doQuest);

        StartCoroutine(KitchenQuest());
    }

    IEnumerator KitchenQuest()
    {
        summonResearcherCount = 12;
        summonNurseCount = 8;

        InstantiateResearcher(summonResearcherCount);
        InstantiateNurse(summonNurseCount);

        while (!isClear)
        {
            if (killedNpcCount >= 20)
            {
                isClear = true;
                doQuest = false;
            }

            yield return new WaitForEndOfFrame();
        }

        SaveManager.Instance.SaveToJson();
        UIManager.Instance.ToggleQuestUI(doQuest);
        yield break;
    }

    private void EnterLaborator()
    {
        Debug.Log("Enter Laborator");
        UIManager.Instance.questTitle.text = "���ο� ��ų";
        UIManager.Instance.questContent.text = "�������� ��Ű�� ����� ��� ���̼���!";

        doQuest = true;

        StartCoroutine(LaboratorQuest());
    }

    IEnumerator LaboratorQuest()
    {
        summonResearcherCount = 15;
        summonSoldierCount = 5;

        InstantiateResearcher(summonResearcherCount);
        InstantiateSoldier(summonSoldierCount);

        while (!isClear)
        {
            if (killedNpcCount >= 20)
            {
                isClear = true;
                doQuest = false;
            }

            yield return new WaitForEndOfFrame();
        }

        SaveManager.Instance.SaveToJson();
        yield break;
    }

    void ServerRoomQuest()
    {
        Debug.Log("Interfere Communication");
        UIManager.Instance.questTitle.text = "�ܺη��� ���� ����";
        UIManager.Instance.questContent.text = "����� �ܺη� ������ ���ϰ� �ֽ��ϴ�. �����ϼ���!";
    }

    void InstantiateResearcher(int researcherCount)
    {
        for (int i = 0; i < researcherCount; ++i)
        {
            Instantiate(researcherPrefab);
        }
    }

    void InstantiateSoldier(int soldierCount)
    {
        for (int i = 0; i < soldierCount; ++i)
        {
            Instantiate(soldierPrefab);
        }
    }

    void InstantiateNurse(int nurseCount)
    {
        for (int i = 0; i < nurseCount; ++i)
        {
            Instantiate(nursePrefab);
        }
    }

}
