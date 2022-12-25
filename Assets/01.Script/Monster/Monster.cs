using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : MonoSingleton<Monster>, IHittable, IAgentStat
{
    float currentExp = 0;
    float levelPerExp = 3;
    int  level = 1;
   public bool isDie = false;
    //public PlayerBase playerBase;
    float maxHp = 100f;
    [SerializeField]
    [Range(0, 100)]
    float currentHp = 100f;
    public bool activeDoorOpen = true;
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
            Debug.Log($"{damage}��ŭ ������");

            BloodSprayEffect.Instance.BloodEffect.transform.SetParent(gameObject.transform);
            BloodSprayEffect.Instance.BloodEffect.transform.localPosition = Vector3.up * 2;
            BloodSprayEffect.Instance.BloodEffect.transform.LookAt(damageDealer.transform);

            BloodSprayEffect.Instance.BloodEffect.Play();
        }

        //���� ������ �¾��� �� 
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
            if(currentHp < 0)
            {
                currentHp = 0;
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

    void Awake()
    {
        CurrentHp = 100f;
        MaxHp = 100f;
    }

    private void Update()
    {
        //print(currentHp);
        //currentHp = Mathf.Clamp(currentHp, 0, MaxHp);
        if (!isDie && SaveManager.Instance.CurrentUser.hp == 0)
        {
            isDie = true;
            onDie?.Invoke();
        }

    }

    void LateUpdate()
    {

        if (currentExp >= levelPerExp)
        {
            level++;
            levelUp?.Invoke(level);
        }

        if(currentHp <= 30 && !QuestManager.Instance.doQuest)
        {
            Debug.Log($"CurrentHP : {currentHp}");
            StartCoroutine(QuestManager.Instance.TreatmentQuest());
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
        maxHp *=  (1f+level/80);
        damage *= (1f + level / 80);
        levelPerExp = level*2+level ;
        if (level == 5)
        {
            activeDoorOpen = true;
        }

    }

}