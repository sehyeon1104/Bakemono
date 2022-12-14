using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("적 종류")]
    [SerializeField] private EnemyType type = EnemyType.Doctor;
    [Header("적 HP")]
    [SerializeField] private int hp;
    [Header("적 이동속도")]
    [SerializeField] private float speed;
    [Header("적 공격력")]
    [SerializeField] private float power;
    [Header("경험치")]
    [SerializeField] private float exp;

    public EnemyType Type => type;

    public int Hp => hp;
    public float Speed => speed;
    public float Powrer => power;
    public float Exp => exp;

}

public enum EnemyType
{
    Doctor,
    Nurse,
    Soldier,
    Boss
}

