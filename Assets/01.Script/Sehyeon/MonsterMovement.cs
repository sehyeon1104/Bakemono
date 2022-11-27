using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    CharacterController monstercontroller;
    Animator monsterAni;
    [SerializeField]
    [Range(0f, 10f)]
    float speed = 5;

    readonly int horizontal = Animator.StringToHash("Horizontal");
    readonly int vertical = Animator.StringToHash("Vertical");
    private void Awake()
    {
      monstercontroller= GetComponent<CharacterController>();
        monsterAni= GetComponent<Animator>();
    }
    public void MoveMonster(Vector3 moveInput)
    {
        monstercontroller.SimpleMove(moveInput*speed);
        monsterAni.SetFloat(horizontal,moveInput.x);
        monsterAni.SetFloat(vertical,moveInput.z);
    }

  
}
