    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MonsterInput : MonoBehaviour
{
    [Header("���Ͱ� �Է¹޴� ������ �ִ� ��ũ��Ʈ")]

    [SerializeField] UnityEvent<Vector3> moveKeyPress;
    [SerializeField] UnityEvent monsterSkill;
    [SerializeField] UnityEvent<float> rotateMouse;
    void Update()
    {
        if (UIManager.Instance.isPause)
        {
            return;
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
            // ������ �ݱ�
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            // ġ��� ȸ��
            GameManager.Instance.HealMonster();
        }

    }
    
}
