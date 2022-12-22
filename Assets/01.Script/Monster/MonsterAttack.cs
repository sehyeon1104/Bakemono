using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Audio;
using Unity.Mathematics;

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
    bool isFind = false;
    bool isComplete = true;
    readonly int leftAttack = Animator.StringToHash("LeftAttack");
    readonly int rightAttack = Animator.StringToHash("RightAttack");
    readonly int IdleNameHash = Animator.StringToHash("Idle");
    readonly int BiteNameHash = Animator.StringToHash("Bite");
    readonly int dieAniHash = Animator.StringToHash("Die");
    readonly int getHit = Animator.StringToHash("Damaged");
    readonly int isDie = Animator.StringToHash("isDie");
    readonly int biteAttack = Animator.StringToHash("BiteAttack");
    bool isLeft = true;
    IHittable agentHit;
    AnimatorStateInfo info;
    Transform imageTrans;
    [SerializeField] TextMeshProUGUI doorTrueText;
    [SerializeField] TextMeshProUGUI doorFalseText;
    Image imageColor;
    RaycastHit hit;
    [SerializeField] AudioClip doorOpen;
    [SerializeField] AudioMixerGroup audioMix;
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    AI_Mob_Default a;
    float timer = 0f;
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
        timer += Time.deltaTime;
        if (!Monster.Instance.isDie)
        {
            Debug.DrawRay(Shootraytrans.position, Shootraytrans.forward * eatDistance, Color.red);
            if (Physics.Raycast(Shootraytrans.position, Shootraytrans.forward, out hit, eatDistance, 1 << LayerMask.NameToLayer("Door")))
            {
                Animation doorAni = hit.transform.parent.GetComponent<Animation>();
                if (!doorAni.isPlaying)
                {
                    if (Monster.Instance.activeDoorOpen)
                    {
                        doorTrueText.gameObject.SetActive(true);
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            hit.transform.parent.GetComponent<Animation>().Play();
                            AudioSource a = hit.transform.gameObject.AddComponent<AudioSource>();
                            a.outputAudioMixerGroup = audioMix;
                            a.PlayOneShot(doorOpen);
                        }
                    }
                    else
                    {
                        doorFalseText.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                doorTrueText.gameObject.SetActive(false);
                doorFalseText.gameObject.SetActive(false);
            }
            if (Physics.Raycast(Shootraytrans.position, Shootraytrans.forward, out hit, eatDistance, 1 << LayerMask.NameToLayer("Enemy")))
            {
                if (hit.transform.GetComponent<AI_Mob_Default>().IsDie)
                    return;
                 a = hit.transform.GetComponent<AI_Mob_Default>();
                if (a.CurrentHp / a.MaxHp < 0.3f && !a.IsDie)
                {
                    if (isComplete)
                    {
                        isComplete = false;
                        isFind = true;
                    }
                    imageColor.DOColor(new Color(0.7f, 0, 0), changeTime);

                        agentHit = hit.transform.GetComponent<IHittable>();
                    if (Input.GetMouseButtonDown(1) && info.shortNameHash == IdleNameHash &&timer>0.4f)
                    {
                        timer = 0;
                        monsterAni.SetTrigger(BiteNameHash);
                        Invoke("Eat", 0.4f);
                        
                     
                    }
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
            if (Input.GetMouseButtonDown(0) && isAttackClick && info.shortNameHash == IdleNameHash && !UIManager.Instance.isPause)
            {
                monsterAttack?.Invoke();
            }
            if (isFind)
            {
                isFind = false;
                imageTrans.DOScale(new Vector3(1.2f, 1.2f, 0), changeTime).SetLoops(2, LoopType.Yoyo).OnComplete(() => isComplete = true);

            }
        }
    }
  
    public void Eat()
    {
        agentHit.GetHit(100,gameObject);
        Monster.Instance.CurrentExp += a.exp;
    }
    public void GetHitAni()
    {
        monsterAni.SetTrigger(getHit);
    }
    public void Attack()
    {
        totalTime = 0;
        isAttackClick = false;
        if (isLeft) //왼쪽 공격
        {
            monsterAni.SetTrigger(leftAttack);
            isLeft = false;
            Invoke("WaitLeftAttack", 0.35f);
        }
        else //오른쪽
        {
            monsterAni.SetTrigger(rightAttack);
            isLeft = true;
            Invoke("WaitRightAttack", 0.35f);
        }
    }

    public void onDie()
    {
        monsterAni.SetBool(isDie, true);
        monsterAni.SetTrigger(dieAniHash);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(leftHand.transform.position, rightHand.GetComponent<BoxCollider>().size / 2);
    }
    void WaitLeftAttack()
    {
        BoxCollider lefthandbox = leftHand.GetComponent<BoxCollider>();
        Collider[] attackCol = Physics.OverlapBox(leftHand.transform.position, lefthandbox.size*2, quaternion.identity, 1 << LayerMask.NameToLayer("Enemy"));
        if (attackCol != null)
        {
            foreach (Collider coll in attackCol)
            {
                IHittable enemyHit = coll.GetComponent<IHittable>();
                enemyHit.GetHit(Monster.Instance.damage, gameObject);
            }
        }
    }
    void WaitRightAttack()
    {
        BoxCollider righthandbox = rightHand.GetComponent<BoxCollider>();
        Collider[] attackCol = Physics.OverlapBox(rightHand.transform.position, righthandbox.size*2, quaternion.identity, 1 << LayerMask.NameToLayer("Enemy"));
        if (attackCol != null)
        {
            foreach (Collider coll in attackCol)
            {
                IHittable enemyHit = coll.GetComponent<IHittable>();
                enemyHit.GetHit(Monster.Instance.damage, gameObject);
            }
        }
    }
}