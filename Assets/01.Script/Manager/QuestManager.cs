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

    [SerializeField]
    private Transform[] kitchenMobSpawnPos;
    [SerializeField]
    private Transform[] LaboratorMobSpawnPos;

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
        UIManager.Instance.ToggleQuestUI(doQuest);

        StartCoroutine(KitchenQuest());
    }

    IEnumerator KitchenQuest()
    {
        summonResearcherCount = 12;
        summonNurseCount = 8;

        InstantiateResearcher(summonResearcherCount, kitchenMobSpawnPos);
        //yield return new WaitForSeconds(0.1f);
        InstantiateNurse(summonNurseCount, kitchenMobSpawnPos);

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
        UIManager.Instance.questTitle.text = "새로운 스킬";
        UIManager.Instance.questContent.text = "연구실을 지키는 사람을 모두 죽이세요!";

        doQuest = true;

        StartCoroutine(LaboratorQuest());
    }

    IEnumerator LaboratorQuest()
    {
        summonResearcherCount = 15;
        summonSoldierCount = 5;

        InstantiateResearcher(summonResearcherCount, LaboratorMobSpawnPos);
        InstantiateSoldier(summonSoldierCount, LaboratorMobSpawnPos);

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
        UIManager.Instance.questTitle.text = "외부로의 교신 방해";
        UIManager.Instance.questContent.text = "사람이 외부로 교신을 전하고 있습니다. 방해하세요!";
    }

    void InstantiateResearcher(int researcherCount, Transform[] spawnPos)
    {
        int pivot = 0;

        while (spawnPos[pivot].childCount != 0)
        {
            pivot++;
        }

        for (int i = pivot; i < researcherCount; ++i)
        {
            Instantiate(researcherPrefab, spawnPos[i]);
        }
    }

    void InstantiateSoldier(int soldierCount, Transform[] spawnPos)
    {
        int pivot = 0;

        while(spawnPos[pivot].childCount != 0)
        {
            pivot++;
        }

        for (int i = pivot; i < soldierCount; ++i)
        {
            Instantiate(soldierPrefab, spawnPos[i]);
        }
    }

    void InstantiateNurse(int nurseCount, Transform[] spawnPos)
    {
        int pivot = 0;

        Debug.Log("InstantiateNurse");

        while (spawnPos[pivot].childCount != 0)
        {
            Debug.Log(pivot);
            pivot++;
        }

        for (int i = pivot; i < pivot + nurseCount; ++i)
        {
            Debug.Log("Instantiate");
            Instantiate(nursePrefab, spawnPos[i]);
        }
    }

}
