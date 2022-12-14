using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI_Mob_Default : MonoBehaviour ,IHittable ,IAgentStat
{
    [Header("Action�� ���ϴ� �Ÿ�")]
    [SerializeField] private float actionDistance;
    [Header("Player �ν� �Ÿ�")]
    [SerializeField] private float scanDistance;
    [Header("Enemy SO")]
    [SerializeField] protected EnemySO enemySO;

    protected NavMeshAgent agent;
    protected Animator anim;

   protected int currentHp;
   protected int maxHp;
   protected float speed;
    public virtual int CurrentHp { get =>currentHp; set=>currentHp=value; }
    public  virtual int MaxHp { get=>maxHp; set=> currentHp = value; }
    public virtual float Speed { get => speed; set => speed=value; }

    public abstract void GetHit(int damage, GameObject damgeDelear);
    public abstract void Action(Transform target);
    public abstract void Move(Vector3 targetPos);
    public abstract void Idle();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        speed = enemySO.Speed;
        maxHp = enemySO.Hp;
        agent.speed = enemySO.Speed;
        currentHp = enemySO.Hp;
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

}
