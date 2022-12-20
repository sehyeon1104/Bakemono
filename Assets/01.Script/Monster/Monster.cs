using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoSingleton<Monster>, IHittable, IAgentStat
{
    float currentExp = 0;
    float levelPerExp = 100;
    int  level = 1;
   public bool isDie = false;
    //public PlayerBase playerBase;
    float maxHp = 100;
    [SerializeField]
    [Range(0, 100)]
    float currentHp = 100;
    public bool activeDoorOpen = false;
    public float damage = 30f;

    [SerializeField] UnityEvent onDie;
    [SerializeField] UnityEvent<int> levelUp;
    [SerializeField] UnityEvent onGethit;
    
    public void GetHit(float damage, GameObject damageDealer)
    {
        if (!isDie)
        {
            onGethit?.Invoke();
            currentHp -= damage;
            Debug.Log($"{damage}만큼 아프다");
        }
        BloodSprayEffect.Instance.BloodEffect.transform.SetParent(gameObject.transform);
        BloodSprayEffect.Instance.BloodEffect.transform.localPosition = Vector3.up * 2;
        BloodSprayEffect.Instance.BloodEffect.transform.LookAt(damageDealer.transform);

        BloodSprayEffect.Instance.BloodEffect.Play();

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

    public float CurrentHp
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
    public int CurrentLevel
    {
        get => level;
        set
        {
            level = value;
        }
    }
    
    public float Speed { get; set; }
    public float MaxHp { get => maxHp;  set => maxHp =value; }

    //void Awake()
    //{
    //    playerBase = new PlayerBase();
    //}

    private void Update()
    {
        print(currentHp);
        currentHp = Mathf.Clamp(currentHp, 0, MaxHp);
    }

    void LateUpdate()
    {

        if(!isDie&&currentHp==0)
        {
            isDie = true;
            onDie?.Invoke();
        }

        if (currentExp >= levelPerExp)
        {
            level++;
            levelUp?.Invoke(level);
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
        if (level == 5)
        {
            activeDoorOpen = true;
        }

    }
    //public void SavePlayerStat()
    //{
    //    SaveManager.Instance.CurrentUser.hp = CurrentHp;
    //    SaveManager.Instance.CurrentUser.experience = CurrentExp;
    //    SaveManager.Instance.CurrentUser.level = level;
    //}
}