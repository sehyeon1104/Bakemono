using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    bool isRight = false;
    public bool isClick = true;
    float totalTime = 0;
    Animator monsterAni;
    readonly int leftAttack = Animator.StringToHash("LeftAttack");
    readonly int rightAttack = Animator.StringToHash("RightAttack");
    readonly int IdleNameHash = Animator.StringToHash("Idle");
    bool isLeft = true;
    void Start()
    {
        monsterAni = GetComponent<Animator>();
    }

    private void Update()
    {
        Attack();
    }
    void Attack()
    {
        totalTime += Time.deltaTime;
        AnimatorStateInfo info = monsterAni.GetCurrentAnimatorStateInfo(1);

        if (totalTime > 0.5f)
        {
            isClick = true;
        }
        if (Input.GetMouseButtonDown(0) && isClick && info.shortNameHash == IdleNameHash)
        {
            totalTime = 0;
            isClick = false;
            if (isLeft) //왼쪽 공격
            {
                monsterAni.SetTrigger(leftAttack);
                isLeft = false;
            }
            else//오른쪽
            {
                monsterAni.SetTrigger(rightAttack);
                isLeft = true;
            }
        }

        if (totalTime > 15f)
        {
            isLeft = true;
        }

    }


}
