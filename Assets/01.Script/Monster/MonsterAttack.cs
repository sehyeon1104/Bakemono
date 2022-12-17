using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField] UnityEvent monsterAttack;
    [SerializeField] UnityEvent findEnemy;
    public Transform Shootraytrans;
    public bool isAttackClick = true;
    [SerializeField]
    float changeTime = 0.7f; 
    float totalTime = 0;
    [SerializeField]
    float deathRate = 0.2f;
    [SerializeField]
    float eatDistance = 1f;
    Animator monsterAni;
    [SerializeField]
    bool doorOpenactive = false; 
    bool isFind = false;
    bool isComplete = true;
    readonly int leftAttack = Animator.StringToHash("LeftAttack");
    readonly int rightAttack = Animator.StringToHash("RightAttack");
    readonly int IdleNameHash = Animator.StringToHash("Idle");
    readonly int BiteNameHash = Animator.StringToHash("Bite");
    bool isLeft = true;
    AnimatorStateInfo info;
    Transform imageTrans;
    [SerializeField] TextMeshProUGUI doorTrueText;
    [SerializeField] TextMeshProUGUI doorFalseText;
    Image imageColor;
    void Start()
    {
    }
    private void Awake()
    {
        monsterAni = GetComponent<Animator>();
        imageTrans = MonsterUI.Instance.skillImage.transform;
        imageColor = MonsterUI.Instance.skillImage;
    }

    private void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(Shootraytrans.position, Shootraytrans.forward * eatDistance, Color.red);
        if (Physics.Raycast(Shootraytrans.position, Shootraytrans.forward, out hit, eatDistance, 1<< LayerMask.NameToLayer("Door")))
        { 
            if(Monster.Instance.activeDoorOpen)
            {
                doorTrueText.enabled = true;
            }
            else
            {
                doorFalseText.enabled = true;
            }
        }
        else
        {
            doorFalseText.enabled = false;
            doorFalseText.enabled = false;
        }
        if (Physics.Raycast(Shootraytrans.position, Shootraytrans.forward, out hit, eatDistance, 1 << LayerMask.NameToLayer("Enemy")))
        {
            if(isComplete)
            {
               
                isComplete = false;
                isFind = true;
            }
            imageColor.DOColor(new Color(0.7f, 0, 0), changeTime);

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
            imageColor.DOColor(new Color(1, 1, 1), changeTime);
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
            isFind = false;
            imageTrans.DOScale(new Vector3(1.2f, 1.2f, 0), changeTime).SetLoops(2, LoopType.Yoyo).OnComplete(() => isComplete = true) ;
       
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