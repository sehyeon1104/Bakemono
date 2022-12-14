using UnityEngine;
using UnityEngine.UI;

public class MonsterUI : MonoBehaviour
{

    Monster monster;
    [SerializeField]
    public Image hpBar;
    [SerializeField]
    public Image attackImg;
    Animator monsterAni;
    AnimatorStateInfo info;
    readonly int idle = Animator.StringToHash("Idle");
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
        info = monsterAni.GetCurrentAnimatorStateInfo(1);

        if (info.shortNameHash != idle)
        {
            attackImg.fillAmount = 1-(info.normalizedTime*info.length-0.25f);
        }
        hpBar.fillAmount = monster.CurrentHp / (float)monster.MaxHp;
    }

}