using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoSingleton<MonsterUI>
{

    Monster monster;
    [SerializeField]
     Image hpBar;
    [SerializeField]
     Image attackImg;
    [SerializeField]
    Image expBar;
    public Image skillImage;
    Animator monsterAni;
    AnimatorStateInfo info;
    readonly int idle = Animator.StringToHash("Idle");
    readonly int bite = Animator.StringToHash("BiteAttack");
    private void Awake()
    {
        monsterAni = GetComponent<Animator>();
        monster = GetComponent<Monster>();
    }
    private void Start()
    {

    }
    void Update()
    {
        UIUpdate();
        info = monsterAni.GetCurrentAnimatorStateInfo(1);

        if ((info.shortNameHash != idle) && (info.shortNameHash !=bite))
        {
            attackImg.fillAmount = 1-(info.normalizedTime*1.25f-0.25f);
        }
        else
        {
            attackImg.fillAmount = 0;
        }
    }

    public void UIUpdate()
    {
        hpBar.fillAmount = monster.CurrentHp / (float)monster.MaxHp;
        expBar.fillAmount = monster.CurrentExp / (float)monster.LevelPerExp;
    }
    public void FindEnemy()
    {
        skillImage.color = Color.red;
    }
}