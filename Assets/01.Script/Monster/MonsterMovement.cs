using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterMovement : MonoSingleton<MonsterMovement> ,IAgentStat
{
    float x;
    float z;
    public Vector3 rotateValue; 
    CharacterController monstercontroller;
    Animator monsterAni;
    [SerializeField]
    [Range(0f, 10f)]
    float speed = 5;
    [SerializeField]
    float animatoinSpeed = 10;
    [SerializeField]
    float rotateAniSpeed = 0.1f;
    [SerializeField]
    float jumpSpeed = 2f;
    [SerializeField]
    float gravity = 5f;
    readonly int jump = Animator.StringToHash("Jump");
    readonly int horizontal = Animator.StringToHash("Horizontal");
    readonly int vertical = Animator.StringToHash("Vertical");
    readonly int headValue = Animator.StringToHash("TurnValue");
    readonly int isTurn = Animator.StringToHash("IsTurn");
    public Vector3 cashed_move = Vector3.zero;
   public float mouseValue =5;
   

    public float CurrentHp { get ; set ; }
    public float MaxHp { get ; set; }
    public float Speed { get => speed; set => speed = value; }
    

    private void Awake()
    {
        monstercontroller = GetComponent<CharacterController>();
        monsterAni = GetComponent<Animator>();
    }
    public void RotateMonster(float rotateInput) 
    {
        rotateInput = Mathf.Clamp(rotateInput, -10, 10);
        monsterAni.SetBool(isTurn, cashed_move.x == 0 && cashed_move.z == 0 && Mathf.Abs(rotateInput) >= float.Epsilon);
        transform.rotation *= Quaternion.Euler(rotateInput * new Vector3(0,mouseValue/2,0)); 
        monsterAni.SetFloat(headValue, rotateInput);
    }
    public void MoveMonster(Vector3 moveInput)
    {
        x = Mathf.Lerp(monsterAni.GetFloat(horizontal), moveInput.x, Time.deltaTime * animatoinSpeed);
        z = Mathf.Lerp(monsterAni.GetFloat(vertical), moveInput.z, Time.deltaTime * animatoinSpeed);
       moveInput = moveInput.normalized;
        if (Mathf.Abs(x) < 0.0001f)
        {
            x = 0;
        }
        if (Mathf.Abs(z) < 0.0001f)
        {
            z = 0;
        }
        monsterAni.SetFloat(horizontal, x);
        monsterAni.SetFloat(vertical, z);

        cashed_move.x = moveInput.x;
        cashed_move.z = moveInput.z;
        
        monstercontroller.Move(transform.rotation*(cashed_move *Time.deltaTime * speed *MonsterInput.Instance.runValue)); 
        if (Input.GetKeyDown(KeyCode.Space)&&monstercontroller.isGrounded)
        {
            monsterAni.SetTrigger(jump);
            cashed_move.y = jumpSpeed;   
        }
        else if(!monstercontroller.isGrounded)
        {
           cashed_move.y -= gravity * Time.deltaTime;
        }

    }

}
