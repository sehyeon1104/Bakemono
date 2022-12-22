using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI_Mob_Default : MonoBehaviour, IHittable
{
    [Header("Action을 취하는 거리")]
    [SerializeField] private float actionDistance;
    [Header("Player 인식 거리")]
    [SerializeField] private float scanDistance;
    [Header("Enemy SO")]
    [SerializeField] protected EnemySO enemySO;

    protected NavMeshAgent agent;
    protected Animator anim;

    protected Coroutine actionCoroutine = null;

    protected readonly int hashMove = Animator.StringToHash("Move");
    protected readonly int hashTrigger = Animator.StringToHash("Trigger");
    protected readonly int hashAttack = Animator.StringToHash("Attack");
    protected readonly int hashHit = Animator.StringToHash("Hit");
    protected readonly int hashDie = Animator.StringToHash("Die");
    
    public float exp;
    protected float currentHp;
    protected float maxHp;

    private bool isHit = false;
    protected bool isDie = false;
    public bool IsDie
    {
        get => isDie;
        set => isDie = value;
    }
    public float CurrentHp => currentHp;
    public float MaxHp => maxHp;    
    public abstract void Action(Transform target);
    public abstract void Move(Vector3 targetPos);
    public abstract void Idle();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        
        maxHp = enemySO.Hp;
        exp = enemySO.Exp;
        currentHp = enemySO.Hp;
        agent.speed = enemySO.Speed;
    }
    private void Update()
    {
        DistanceCheck();
    }

    public void DistanceCheck()
    {
        if (MainModule.player == null) return;
        if (isDie || isHit) return;

        Transform playerPos = MainModule.player.transform;

        switch (Vector3.Distance(transform.position, playerPos.position))
        {
            case var a when a <= actionDistance:
                Action(playerPos);
                break;

            case var a when a > actionDistance && a <= scanDistance:
                Move(playerPos.position);
                break;

            default:
                Idle();
                break;
        }

    }
 
    public void GetHit(float damage, GameObject damageDealer)
    {
        isHit = true;
        currentHp -= damage;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;

        if (actionCoroutine != null)
        {
            StopCoroutine(actionCoroutine);
            actionCoroutine = null;
        }

        if (currentHp <= 0 && !isDie)
        {
            isDie = true;
            agent.isStopped = true;
            agent.speed = 0;
            agent.angularSpeed = 0;
            agent.velocity = Vector3.zero;

            anim.SetBool(hashDie, true);
            anim.SetTrigger(hashTrigger);
            BloodSprayEffect.Instance.BloodEffect.transform.SetParent(null);
            Destroy(gameObject, 3f);
        }

        if (!isDie)
        {
            BloodSprayEffect.Instance.BloodEffect.transform.SetParent(gameObject.transform);
            BloodSprayEffect.Instance.BloodEffect.transform.localPosition = Vector3.up * 1.5f;
            BloodSprayEffect.Instance.BloodEffect.transform.SetParent(null);
            BloodSprayEffect.Instance.BloodEffect.transform.LookAt(damageDealer.transform);
            BloodSprayEffect.Instance.BloodEffect.Play();
        }
        
        anim.SetTrigger(hashHit, ()=>
        {
            isHit = false;
        }
        ,0.75f);
    }

}
