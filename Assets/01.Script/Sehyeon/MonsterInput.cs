using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MonsterInput : MonoBehaviour
{
    [Header("몬스터가 입력받는 모든것을 넣는 스크립트")]

    [SerializeField] UnityEvent<Vector3> moveKeyPress;
    [SerializeField] UnityEvent AttackButtonPress;
    void Update()
    {
        MonsterMove();  
    }
    void MonsterMove()
    {
        moveKeyPress?.Invoke(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized);
    }
    
}
