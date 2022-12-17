using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoSingleton<Monster>, IHittable, IAgentStat
{
    float currentExp = 0;
    float levelPerExp = 100;
    int  level = 1;
    bool isDie = false;
    public PlayerBase playerBase;
    int maxHp = 100;
    [SerializeField]
    [Range(0, 100)]
    int currentHp = 100;
    public bool activeDoorOpen = true;
    [SerializeField] UnityEvent onDie;
    [SerializeField] UnityEvent<int> levelUp;
    [SerializeField] UnityEvent onGethit;
    
    public void GetHit(int damage, GameObject damgeDelear)
    {
        //대충 적한테 맞았을 때 
    }
    public float LevelPerExp
    {
        get => levelPerExp;
        set => levelPerExp = value;
    }
    public float CurrentExp
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
        set
        {
            currentHp = value;

            if (currentHp > maxHp)
            {
                currentHp = maxHp;
            }

        }

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
    public void Levelactive(int level)
    {
        levelPerExp *= 1.2f;
        if(level == 5)
        {
            activeDoorOpen= true;
        }    

    }
}