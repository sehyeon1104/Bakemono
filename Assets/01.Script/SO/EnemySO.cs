using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("�� ����")]
    [SerializeField] private EnemyType type = EnemyType.Doctor;
    [Header("�� HP")]
    [SerializeField] private int hp;
    [Header("�� �̵��ӵ�")]
    [SerializeField] private float speed;
    [Header("�� ���ݷ�")]
    [SerializeField] private float power;
    [Header("����ġ")]
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

