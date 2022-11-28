using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    float x;
    float z;
    CharacterController monstercontroller;
    Animator monsterAni;
    [SerializeField]
    [Range(0f, 10f)]
    float speed = 5;
    [SerializeField]
    float animatoinSpeed = 10;
    readonly int horizontal = Animator.StringToHash("Horizontal");
    readonly int vertical = Animator.StringToHash("Vertical");
    private void Awake()
    {
      monstercontroller= GetComponent<CharacterController>();
        monsterAni= GetComponent<Animator>();
    }
    public void MoveMonster(Vector3 moveInput)
    {
        x = Mathf.Lerp(monsterAni.GetFloat(horizontal), moveInput.x, Time.deltaTime * animatoinSpeed);
        z = Mathf.Lerp(monsterAni.GetFloat(vertical), moveInput.z, Time.deltaTime*animatoinSpeed);
        if(Mathf.Abs(x)<0.0001f)
        {
            x = 0;
        }
        if(Mathf.Abs(z)<0.0001f)
        {
            z = 0;
        }
        monsterAni.SetFloat(horizontal,x);
        monsterAni.SetFloat(vertical,z);
        monstercontroller.SimpleMove(moveInput.normalized * speed);
    }


  
}
