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

    public List<GameObject> researcherFemaleSpawnPos = new List<GameObject>();
    public List<GameObject> soldierSpawnPos = new List<GameObject>();
    public List<GameObject> researcherMaleSpawnPos = new List<GameObject>();

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

    void InstantiateResearcher(List<GameObject> spawnPos)
    {
        for (int i = 0; i < spawnPos.Count; ++i)
        {
            GameObject rMale = Instantiate(researcherMalePrefab, spawnPos[i].transform);
            rMale.transform.SetParent(spawnPos[i].transform);
            Debug.Log(rMale.transform.parent.position);
        }
    }

    void InstantiateSoldier(List<GameObject> spawnPos)
    {
        for (int i = 0; i < spawnPos.Count; ++i)
        {
            GameObject rFemale = Instantiate(soldierPrefab, spawnPos[i].transform);
            rFemale.transform.SetParent(spawnPos[i].transform);
            Debug.Log(rFemale.transform.parent.position);
        }
    }

    void InstantiateNurse(List<GameObject> spawnPos)
    {
        for (int i = 0; i < spawnPos.Count; ++i)
        {
            GameObject soldier = Instantiate(researcherFemalePrefab, spawnPos[i].transform);
            soldier.transform.SetParent(spawnPos[i].transform);
            Debug.Log(soldier.transform.parent.position);
        }
    }

}
