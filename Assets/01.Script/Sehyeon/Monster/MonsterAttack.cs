using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    bool isRight = false;
    bool isClick = true;
    bool once = true;
    float totalTime = 0;
    float animTime = 0;
    Animator monsterAni;
    readonly int leftAttack = Animator.StringToHash("LeftAttack");
    readonly int rightAttack = Animator.StringToHash("RightAttack");
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
        if (Input.GetMouseButtonDown(0) && isClick == true)
        {
            isClick = false;
            totalTime = 0;
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
        if (monsterAni.GetCurrentAnimatorStateInfo(1).normalizedTime >= 0.99f)
        {
            isClick = true;
        }
        if (totalTime > 15f)
        {
            isLeft = true;
        }

    }


}
