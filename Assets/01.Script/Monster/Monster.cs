using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoSingleton<Monster>, IHittable , IAgentStat
{
    int currentExp = 10;
    int levelPerExp = 0;
    int level = 1;
    bool isDie = false;
    public PlayerBase playerBase;
    int maxHp = 100;
    [SerializeField]
    [Range(0,100)]
    int currentHp = 100;
    [SerializeField] UnityEvent onDie;
    [SerializeField] UnityEvent<int> levelUp;
    [SerializeField] UnityEvent onGethit;
    public void GetHit(int damage, GameObject damgeDelear)
    {
        //대충 적한테 맞았을 때 
    }
    public int LevelPerExp
    {
        get => levelPerExp;
        set => levelPerExp = value;
    }
    public int CurrentExp
    {
        get => currentExp;
        set => currentExp = value;
    }
    public int MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }
    public int CurrentHp
    {
        get => currentHp;
        set => currentHp = value;
    }
    public float Speed { get; set; }

    void Awake()
    {
        playerBase = new PlayerBase();

    }
    private void Start()
    {

    }

    void LateUpdate()
    {
        if (currentExp >= levelPerExp)
        {
            level++;

            levelUp?.Invoke(level);
        }
        if (!isDie)
        {
            playerBase.LevelPerExp = levelPerExp;
            playerBase.Exp = currentExp;
            playerBase.Level = level;
            playerBase.HP = currentHp;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Quest") && !QuestManager.Instance.doQuest)
        {
            QuestManager.Instance.SendMessage(other.name);
            Destroy(other.gameObject);
        }
    }
}