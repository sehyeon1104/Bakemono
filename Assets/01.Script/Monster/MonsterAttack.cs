using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
public class MonsterAttack : MonoBehaviour
{
    [SerializeField] UnityEvent monsterAttack;
    [SerializeField] UnityEvent findEnemy;
    public Transform Shootraytrans;
    public bool isAttackClick = true;
    float totalTime = 0;
    [SerializeField]
    float deathRate = 0.2f;
    [SerializeField]
    float eatDistance = 1f;
    Animator monsterAni;
    bool isFind = false;
    readonly int leftAttack = Animator.StringToHash("LeftAttack");
    readonly int rightAttack = Animator.StringToHash("RightAttack");
    readonly int IdleNameHash = Animator.StringToHash("Idle");
    readonly int BiteNameHash = Animator.StringToHash("Bite");
    Sequence dotSequence;
    bool isLeft = true;
    AnimatorStateInfo info;
    Transform imageTrans;


    void Start()
    {
        dotSequence = DOTween.Sequence();


    }
    private void Awake()
    {
        monsterAni = GetComponent<Animator>();
        imageTrans = MonsterUI.Instance.skillImage.transform;
    }

    private void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(Shootraytrans.position, Shootraytrans.forward * eatDistance, Color.red);
        if (Physics.Raycast(Shootraytrans.position, Shootraytrans.forward, out hit, eatDistance, 1 << LayerMask.NameToLayer("Enemy")))
        {
            isFind=true;
            MonsterUI.Instance.skillImage.color = Color.red;

            IAgentStat agentStat = hit.transform.GetComponent<IAgentStat>();
            if (Input.GetMouseButtonDown(1) && info.shortNameHash == IdleNameHash)
            {
                monsterAni.SetTrigger(BiteNameHash);
            }
        }
        else
        {
            isFind = false;
            MonsterUI.Instance.skillImage.color = Color.white;
        }

        info = monsterAni.GetCurrentAnimatorStateInfo(1);
        totalTime += Time.deltaTime;
        if (totalTime > 0.5f)
        {
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
        if(isFind)
        {
            imageTrans.DOScale(new Vector3(2, 2, 0), 1f).SetLoops(2, LoopType.Yoyo);
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