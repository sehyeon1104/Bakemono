using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MonsterInput : MonoBehaviour
{
    [Header("몬스터가 입력받는 모든것을 넣는 스크립트")]
    bool isLeft =true;
    [SerializeField] UnityEvent<Vector3> moveKeyPress;
    [SerializeField] UnityEvent<bool> attackButtonPress;
    void Update()
    {
       
        MonsterMove();
        if(Input.GetMouseButtonDown(0))
        {
            isLeft = !isLeft;
            attackButtonPress?.Invoke(isLeft);
        }
    }
   public void MonsterMove()
    {
        moveKeyPress?.Invoke(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized);
    }
    
    
}
