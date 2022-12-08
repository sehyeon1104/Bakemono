using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("적 종류")]
    [SerializeField] private EnemyType type = EnemyType.Doctor;
    [Header("적 HP")]
    [SerializeField] private float hp;
    [Header("적 이동속도")]
    [SerializeField] private float speed;

    public EnemyType Type => type;
    public float Hp => hp;
    public float Speed => speed;

}

public enum EnemyType
{
    Doctor,
    Nurse,
    Soldier,
    Boss
}

