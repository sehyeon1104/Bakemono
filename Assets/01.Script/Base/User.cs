using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User 
{
    public float maxHp;
    public float hp;
    public float experience;
    public int level;
    public List<GameObject> researcherFemaleSpawnPos;
    public List<GameObject> soldierSpawnPos;
    public List<GameObject> researcherMaleSpawnPos;
}
