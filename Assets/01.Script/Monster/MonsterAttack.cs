using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] UnityEvent monsterAttack;
    [SerializeField]
    bool isRight = false;
    public bool isClick = true;
    float totalTime = 0;
    Animator monsterAni;
    readonly int leftAttack = Animator.StringToHash("LeftAttack");
    readonly int rightAttack = Animator.StringToHash("RightAttack");
    readonly int IdleNameHash = Animator.StringToHash("Idle");
    bool isLeft = true;
    AnimatorStateInfo info;
    void Start()
    {
        monsterAni = GetComponent<Animator>();
    }

    private void Update()
    {
        info = monsterAni.GetCurrentAnimatorStateInfo(1);
        totalTime += Time.deltaTime;
        if (totalTime > 0.5f)
        {
            isClick = true;
        }
        if (totalTime > 15f)
        {
            isLeft = true;
        }
        if (Input.GetMouseButtonDown(0) && isClick && info.shortNameHash == IdleNameHash)
        {
            monsterAttack?.Invoke();
        }
    }
    public void Attack()
    {
        totalTime = 0;
        isClick = false;
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
}