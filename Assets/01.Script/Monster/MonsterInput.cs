    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MonsterInput : MonoBehaviour
{
    [Header("몬스터가 입력받는 모든것을 넣는 스크립트")]

    [SerializeField] UnityEvent<Vector3> moveKeyPress;
    [SerializeField] UnityEvent monsterSkill;
    [SerializeField] UnityEvent<float> rotateMouse;
    [SerializeField] private GameObject doorLock = null;
    [SerializeField] private float doorLockDis = 5f;

    [SerializeField]
    private void Update()
    {
        if (UIManager.Instance.isPause)
        {
            return;
        }

        if (UIManager.Instance.passwordPanel.activeSelf)
        {
            if (Vector3.Distance(doorLock.transform.position, transform.position) > doorLockDis)
            {
                UIManager.Instance.TogglePasswordPanel(false);
            }
        }

        MonsterMove();
        MonsterRotate();
        InputKey();
    }
    public void MonsterRotate()
    {
        rotateMouse?.Invoke(Input.GetAxisRaw("Mouse X"));
    }
   public void MonsterMove()
    {   
        moveKeyPress?.Invoke((new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))).normalized);
    }
    public void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(Vector3.Distance(doorLock.transform.position, transform.position));
            // 문열고 닫기
            if(Vector3.Distance(doorLock.transform.position, transform.position) < doorLockDis)
            {
                UIManager.Instance.TogglePasswordPanel(!UIManager.Instance.passwordPanel.activeSelf);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 치료실 회복
            GameManager.Instance.HealMonster();
        }

    }
    
}
