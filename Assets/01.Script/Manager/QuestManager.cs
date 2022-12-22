using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoSingleton<QuestManager>
{
    public bool doQuest { private set; get; } = false;
    public bool isClear { private set; get; } = false;

    public int killedNpcCount = 0;
    [SerializeField]
    private GameObject researcherMalePrefab = null;
    [SerializeField]
    private GameObject soldierPrefab = null;
    [SerializeField]
    private GameObject researcherFemalePrefab = null;
    [SerializeField]
    private int summonResearcherMaleCount = 0;
    [SerializeField]
    private int summonSoldierCount = 0;
    [SerializeField]
    private int summonResearcherFemaleCount = 0;

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
        UIManager.Instance.questTitle.text = "Tasty People";
        UIManager.Instance.questContent.text = "Defeat all people.";

        isClear = false;
        doQuest = true;
        UIManager.Instance.ToggleQuestUI(doQuest);

        StartCoroutine(KitchenQuest());
    }

    IEnumerator KitchenQuest()
    {
        summonResearcherMaleCount = 12;
        summonResearcherFemaleCount = 8;

        InstantiateResearcher(summonResearcherMaleCount, kitchenMobSpawnPos);
        InstantiateNurse(summonResearcherFemaleCount, kitchenMobSpawnPos);

        while (!isClear)
        {
            for(int i = 0; i < kitchenMobSpawnPos.Length; ++i)
            {
                if(kitchenMobSpawnPos[i].childCount != 0)
                {
                    break;
                }

                if(i == kitchenMobSpawnPos.Length - 1)
                {
                    isClear = true;
                    doQuest = false;
                }
            }

            yield return new WaitForEndOfFrame();
        }

        isClear = false;
        SaveManager.Instance.SaveToJson();
        UIManager.Instance.ToggleQuestUI(doQuest);
        yield break;
    }

    private void EnterLaborator()
    {
        Debug.Log("Enter Laborator");
        UIManager.Instance.questTitle.text = "New Skill..?";
        UIManager.Instance.questContent.text = "Defeat all people \n who guard a laborator.";

        isClear = false;
        doQuest = true;

        UIManager.Instance.ToggleQuestUI(doQuest);
        StartCoroutine(LaboratorQuest());
    }

    IEnumerator LaboratorQuest()
    {
        summonResearcherMaleCount = 15;
        summonSoldierCount = 5;

        InstantiateResearcher(summonResearcherMaleCount, LaboratorMobSpawnPos);
        InstantiateSoldier(summonSoldierCount, LaboratorMobSpawnPos);

        while (!isClear)
        {

            for (int i = 0; i < LaboratorMobSpawnPos.Length; ++i)
            {
                if (LaboratorMobSpawnPos[i].childCount != 0)
                {
                    break;
                }

                if (i == LaboratorMobSpawnPos.Length - 1)
                {
                    isClear = true;
                    doQuest = false;
                }
            }

            yield return new WaitForEndOfFrame();
        }

        isClear = false;
        SaveManager.Instance.SaveToJson();
        UIManager.Instance.ToggleQuestUI(doQuest);
        yield break;
    }

    void ServerRoomQuest()
    {
        Debug.Log("Interfere Communication");
        UIManager.Instance.questTitle.text = "Interfere with communication";
        UIManager.Instance.questContent.text = "A person is communicating to the outside. \nGet in the way!";
    }

    public IEnumerator TreatmentQuest()
    {
        Debug.Log("Treatment Quest");
        isClear = false;
        doQuest = true;
        UIManager.Instance.questTitle.text = "Heal!!";
        UIManager.Instance.questContent.text = "Go to treatment room to heal your hp";
        UIManager.Instance.ToggleQuestUI(true);

        while (!isClear)
        {
            if (Monster.Instance.CurrentHp >= 100)
            {
                Debug.Log("clear treatment quest");
                isClear = true;
                doQuest = false;
            }

            yield return new WaitForEndOfFrame();
        }

        isClear = false;
        UIManager.Instance.ToggleQuestUI(doQuest);
    }

    void InstantiateResearcher(int researcherCount, Transform[] spawnPos)
    {
        int pivot = 0;

        while (spawnPos[pivot].childCount != 0 && pivot < spawnPos.Length)
        {
            pivot++;
        }

        for (int i = pivot; i < pivot + researcherCount; ++i)
        {
            EnemySpawnManager.Instance.InstantiateMaleResearcher(spawnPos[i]);
            //Instantiate(researcherMalePrefab, spawnPos[i]);
        }
    }

    void InstantiateSoldier(int soldierCount, Transform[] spawnPos)
    {
        int pivot = 0;

        while(spawnPos[pivot].childCount != 0 && pivot < spawnPos.Length)
        {
            pivot++;
        }

        for (int i = pivot; i < pivot + soldierCount; ++i)
        {
            EnemySpawnManager.Instance.InstantiateSoldier(spawnPos[i]);
            //Instantiate(soldierPrefab, spawnPos[i]);
        }
    }

    void InstantiateNurse(int nurseCount, Transform[] spawnPos)
    {
        int pivot = 0;

        Debug.Log("InstantiateFemaleResearcher");

        while (spawnPos[pivot].childCount != 0 && pivot < spawnPos.Length)
        {
            Debug.Log(pivot);
            pivot++;
        }

        for (int i = pivot; i < pivot + nurseCount; ++i)
        {
            EnemySpawnManager.Instance.InstantiateFemaleResearcher(spawnPos[i]);
            //Debug.Log("Instantiate");
            //Instantiate(researcherFemalePrefab, spawnPos[i]);
        }
    }

}
