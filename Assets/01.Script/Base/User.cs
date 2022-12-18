using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User 
{
    public int maxHp;
    public int hp;
    public float experience;
    public int level;
    public List<Transform> researcherFemaleSpawnPos;
    public List<Transform> soldierSpawnPos;
    public List<Transform> researcherMaleSpawnPos;
}
