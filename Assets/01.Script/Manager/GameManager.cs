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
            EnemySpawnManager.Instance.InstantiateMaleResearcher(spawnPos[i]);
            //GameObject rMale = Instantiate(researcherMalePrefab);
            //rMale.transform.position = spawnPos[i];
            //rMale.transform.SetParent(spawnPos[i].transform);
            //Debug.Log(rMale.transform.parent.position);
        }
    }

    void InstantiateSoldier(List<Transform> spawnPos)
    {
        for (int i = 0; i < spawnPos.Count; ++i)
        {
            EnemySpawnManager.Instance.InstantiateFemaleResearcher(spawnPos[i]);
            //GameObject rFemale = Instantiate(soldierPrefab);
            //rFemale.transform.position = spawnPos[i].transform.position;
            //rFemale.transform.SetParent(spawnPos[i].transform);
            //Debug.Log(rFemale.transform.parent.position);
        }
    }

    void InstantiateNurse(List<Transform> spawnPos)
    {
        for (int i = 0; i < spawnPos.Count; ++i)
        {
            EnemySpawnManager.Instance.InstantiateSoldier(spawnPos[i]);
            //GameObject soldier = Instantiate(researcherFemalePrefab);
            //soldier.transform.position = spawnPos[i].transform.position;
            //soldier.transform.SetParent(spawnPos[i].transform);
            //Debug.Log(soldier.transform.parent.position);
        }
    }

}
