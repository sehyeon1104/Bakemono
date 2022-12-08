using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptables/Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("�� ����")]
    [SerializeField] private EnemyType type = EnemyType.Doctor;
    [Header("�� HP")]
    [SerializeField] private float hp;
    [Header("�� �̵��ӵ�")]
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

