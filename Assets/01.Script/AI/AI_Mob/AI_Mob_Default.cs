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

    [SerializeField] private ParticleSystem bloodParticle;

    protected NavMeshAgent agent;
    protected Animator anim;

    protected Coroutine actionCoroutine = null;

    protected readonly int hashMove = Animator.StringToHash("Move");
    protected readonly int hashTrigger = Animator.StringToHash("Trigger");
    protected readonly int hashAttack = Animator.StringToHash("Attack");
    protected readonly int hashHit = Animator.StringToHash("Hit");

    protected float currentHp;

    public float CurrentHp => currentHp;

    public abstract void Action(Transform target);
    public abstract void Move(Vector3 targetPos);
    public abstract void Idle();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

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
        Debug.Log("s");
        currentHp -= damage;

        bloodParticle.transform.SetParent(gameObject.transform);
        bloodParticle.transform.localPosition = Vector3.up * 2;
        bloodParticle.transform.LookAt(damageDealer.transform);

        bloodParticle.Play();
        
        anim.SetTrigger(hashHit);
    }

}
