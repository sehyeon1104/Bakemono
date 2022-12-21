using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
{
    [SerializeField]
    private AI_Mob_Doctor maleResearcherPre;
    [SerializeField]
    private AI_Mob_Nurse femaleResearcherPre;
    [SerializeField]
    private AI_Mob_Soldier soldierPre;

    public IObjectPool<AI_Mob_Doctor> maleResearcherPool;
    public IObjectPool<AI_Mob_Soldier> soldierPool;
    public IObjectPool<AI_Mob_Nurse> femaleResearcherPool;

    private void Awake()
    {
        maleResearcherPool = new ObjectPool<AI_Mob_Doctor>(
            CreateMaleResearcher,
            OnSpawnMaleResearcher,
            OnDespawnMaleResearcher,
            OnDestroyedMaleResearcher,
            maxSize: 20
            );

        soldierPool = new ObjectPool<AI_Mob_Soldier>(
            CreateSoldier,
            OnSpawnSoldier,
            OnDespawnSoldier,
            OnDestroyedSoldier,
            maxSize: 10
            );

        femaleResearcherPool = new ObjectPool<AI_Mob_Nurse>(
            CreateFemaleResearcher,
            OnSpawnFemaleResearcher,
            OnDespawnFemaleResearcher,
            OnDestroyedFemaleResearcher,
            maxSize: 20
            );
    }

    #region MaleResearcher
    public void InstantiateMaleResearcher(Transform spawnPos)
    {
        maleResearcherPool.Get();
        maleResearcherPool.Get().transform.position = spawnPos.position;
        maleResearcherPool.Get().transform.SetParent(spawnPos);
    }

    private AI_Mob_Doctor CreateMaleResearcher()
    {
        AI_Mob_Doctor maleResearcher = Instantiate(maleResearcherPre);
        maleResearcher.SetPool(maleResearcherPool);
        return maleResearcher;
    }

    private void OnSpawnMaleResearcher(AI_Mob_Doctor maleResearcher)
    {
        maleResearcher.transform.position = Vector3.zero;
        maleResearcher.gameObject.SetActive(true);
    }

    private void OnDespawnMaleResearcher(AI_Mob_Doctor maleResearcher)
    {
        maleResearcher.gameObject.SetActive(false);
    }

    private void OnDestroyedMaleResearcher(AI_Mob_Doctor maleResearcher)
    {
        Destroy(maleResearcher);
    }
    #endregion

    #region FemaleResearcher
    public void InstantiateFemaleResearcher(Transform spawnPos)
    {
        femaleResearcherPool.Get();
        femaleResearcherPool.Get().transform.position = spawnPos.transform.position;
        femaleResearcherPool.Get().transform.SetParent(spawnPos);
    }

    private AI_Mob_Nurse CreateFemaleResearcher()
    {
        AI_Mob_Nurse femaleResearcher = Instantiate(femaleResearcherPre);
        femaleResearcher.SetPool(femaleResearcherPool);
        return femaleResearcher;
    }

    private void OnSpawnFemaleResearcher(AI_Mob_Nurse femaleResearcher)
    {
        femaleResearcher.transform.position = Vector3.zero;
        femaleResearcher.gameObject.SetActive(true);
    }

    private void OnDespawnFemaleResearcher(AI_Mob_Nurse femaleResearcher)
    {
        femaleResearcher.gameObject.SetActive(false);
    }

    private void OnDestroyedFemaleResearcher(AI_Mob_Nurse maleResearcher)
    {
        Destroy(maleResearcher);
    }
    #endregion

    #region Soldier
    public void InstantiateSoldier(Transform spawnPos)
    {
        maleResearcherPool.Get();
        maleResearcherPool.Get().transform.position = spawnPos.transform.position;
        maleResearcherPool.Get().transform.SetParent(spawnPos);
    }

    private AI_Mob_Soldier CreateSoldier()
    {
        AI_Mob_Soldier soldier = Instantiate(soldierPre);
        soldier.SetPool(soldierPool);
        return soldier;
    }

    private void OnSpawnSoldier(AI_Mob_Soldier soldier)
    {
        soldier.transform.position = Vector3.zero;
        soldier.gameObject.SetActive(true);
    }

    private void OnDespawnSoldier(AI_Mob_Soldier soldier)
    {
        soldier.gameObject.SetActive(false);
    }

    private void OnDestroyedSoldier(AI_Mob_Soldier soldier)
    {
        Destroy(soldier);
    }
    #endregion
}
