using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    CharacterController monstercontroller;
    [SerializeField]
    [Range(0f,10f)]
    float speed;
    private void Awake()
    {
      monstercontroller= GetComponent<CharacterController>();
    }
    public void MoveMonster(Vector3 moveInput)
    {
        monstercontroller.SimpleMove(moveInput*speed);
    }

  
}
