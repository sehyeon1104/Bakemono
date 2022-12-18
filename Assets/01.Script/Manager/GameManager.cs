using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Treatment Room")]
    readonly float healDelay = 180f;
    private float curDelay = 180f;

    [Header("SpawnEnemy")]
    [SerializeField]
    private GameObject researcherMalePrefab = null;
    [SerializeField]
    private GameObject soldierPrefab = null;
    [SerializeField]
    private GameObject researcherFemalePrefab = null;
    [SerializeField]
    private int summonResearcherMaleCount = 10;
    [SerializeField]
    private int summonSoldierCount = 3;
    [SerializeField]
    private int summonResearcherFemaleCount = 3;

    // 10
    public List<Transform> researcherFemaleSpawnPos = new List<Transform>();
    // 3
    public List<Transform> soldierSpawnPos = new List<Transform>();
    // 3
    public List<Transform> researcherMaleSpawnPos = new List<Transform>();

    private void Awake()
    {
        MouseManager.Lock(true);
        MouseManager.Visible(true);
    }

    private void Start()
    {
        InstantiateResearcher(summonResearcherMaleCount, researcherMaleSpawnPos);
        InstantiateNurse(summonResearcherFemaleCount, researcherFemaleSpawnPos);
        InstantiateSoldier(summonSoldierCount, soldierSpawnPos);
    }

    private void Update()
    {
        if(curDelay >= 180f)
        {
            return;
        }

        curDelay += Time.deltaTime;
    }

    public void HealMonster()
    {
        if(curDelay < 180f)
        {
            return;
        }

        Debug.Log("Heal");
        curDelay = 0f;
        Monster.Instance.CurrentHp = Monster.Instance.MaxHp;
    }

    void InstantiateResearcher(int researcherCount, List<Transform> spawnPos)
    {
        for (int i = 0; i < researcherCount; ++i)
        {
            Instantiate(researcherMalePrefab, spawnPos[i]);
        }
    }

    void InstantiateSoldier(int soldierCount, List<Transform> spawnPos)
    {
        for (int i = 0; i < soldierCount; ++i)
        {
            Instantiate(soldierPrefab, spawnPos[i]);
        }
    }

    void InstantiateNurse(int nurseCount, List<Transform> spawnPos)
    {
        for (int i = 0; i < nurseCount; ++i)
        {
            Instantiate(researcherFemalePrefab, spawnPos[i]);
        }
    }

}
