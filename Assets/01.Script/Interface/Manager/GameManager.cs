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

    public List<Transform> researcherFemaleSpawnPos = new List<Transform>();
    public List<Transform> soldierSpawnPos = new List<Transform>();
    public List<Transform> researcherMaleSpawnPos = new List<Transform>();

    private void Awake()
    {
        MouseManager.Lock(true);
        MouseManager.Visible(true);
    }

    private void Start()
    {
        InstantiateResearcher(researcherMaleSpawnPos);
        InstantiateNurse(researcherFemaleSpawnPos);
        InstantiateSoldier(soldierSpawnPos);
        Fade.Instance.FadeOut();
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

    void InstantiateResearcher(List<Transform> spawnPos)
    {
        for (int i = 0; i < spawnPos.Count; ++i)
        {
            Instantiate(researcherMalePrefab, spawnPos[i]);
        }
    }

    void InstantiateSoldier(List<Transform> spawnPos)
    {
        for (int i = 0; i < spawnPos.Count; ++i)
        {
            Instantiate(soldierPrefab, spawnPos[i]);
        }
    }

    void InstantiateNurse(List<Transform> spawnPos)
    {
        for (int i = 0; i < spawnPos.Count; ++i)
        {
            Instantiate(researcherFemalePrefab, spawnPos[i]);
        }
    }

}
