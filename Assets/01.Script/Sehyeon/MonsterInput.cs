using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MonsterInput : MonoBehaviour
{
    Animator monsterAnimtor;
    [Header("���Ͱ� �Է¹޴� ������ �ִ� ��ũ��Ʈ")]
    CharacterController monsterController;
    [SerializeField] UnityEvent<Vector3> moveKeyPress;
    private void Awake()
    {
        monsterAnimtor= GetComponent<Animator>();
        monsterController= GetComponent<CharacterController>();
    }
    void Update()
    {
        MonsterMove();  
    }
    void MonsterMove()
    {
        moveKeyPress?.Invoke(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")));
        monsterAnimtor.SetBool("Walk", true);
    }
    
}
