using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] UnityEvent monsterAttack;
    [SerializeField] UnityEvent findEnemy;
    public Transform Shootraytrans;
    public bool isAttackClick = true;
    public bool isBiteClick = true;
    float totalTime = 0;
    [SerializeField]
    float eatDistance = 1f;
    Animator monsterAni;
    readonly int leftAttack = Animator.StringToHash("LeftAttack");
    readonly int rightAttack = Animator.StringToHash("RightAttack");
    readonly int IdleNameHash = Animator.StringToHash("Idle");
    readonly int BiteNameHash = Animator.StringToHash("Bite");
    bool isLeft = true;
    AnimatorStateInfo info;



    void Start()
    {
        monsterAni = GetComponent<Animator>();
    }

    private void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(Shootraytrans.position, Shootraytrans.forward * eatDistance, Color.red);
        if (Physics.Raycast(Shootraytrans.position, Shootraytrans.forward, out hit, eatDistance, 1 << LayerMask.NameToLayer("Enemy")))
        {

            if (Input.GetMouseButtonDown(1) && info.shortNameHash == IdleNameHash)
            {
                isBiteClick = false;
                monsterAni.SetTrigger(BiteNameHash);
            }

        }
        info = monsterAni.GetCurrentAnimatorStateInfo(1);
        totalTime += Time.deltaTime;
        if (totalTime > 0.5f)
        {
            isBiteClick = true;
            isAttackClick = true;
        }
        if (totalTime > 15f)
        {
            isLeft = true;
        }
        if (Input.GetMouseButtonDown(0) && isAttackClick && info.shortNameHash == IdleNameHash)
        {
            monsterAttack?.Invoke();
        }
    }
    public void Attack()
    {
        totalTime = 0;
        isAttackClick = false;
        if (isLeft) //왼쪽 공격
        {
            monsterAni.SetTrigger(leftAttack);
            isLeft = false;
        }
        else //오른쪽
        {
            monsterAni.SetTrigger(rightAttack);
            isLeft = true;
        }
    }
    public void Bite()
    {

    }
}