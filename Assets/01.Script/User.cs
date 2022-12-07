using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class User
{
    public int maxHp = 10;
    public int hp = 10;
    public float experience = 0f;
    public int level = 0;
    public int[] maxExperience = { 1, 2, 3, 4, 5 };
    public float attack = 20f;
    public float moveSpeed = 10f;
}
