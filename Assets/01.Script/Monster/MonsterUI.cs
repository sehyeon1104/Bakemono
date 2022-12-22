using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using DG.Tweening;
public class MonsterUI : MonoSingleton<MonsterUI>
{
    public Slider audioSlider;
    public Slider mouseSlider;
    [SerializeField]
    AudioMixer AudioMixer;
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
    [SerializeField]
    TextMeshProUGUI hpText;
    [SerializeField]
    TextMeshProUGUI expText;
    [SerializeField]
    TextMeshProUGUI LvText;
    readonly int idle = Animator.StringToHash("Idle");
    readonly int bite = Animator.StringToHash("BiteAttack");
    private void Awake()
    {
        monsterAni = GetComponent<Animator>();
        monster = GetComponent<Monster>();
    }
    private void Start()
    {
        LvText.DOFade(0, 0);
        AudioMixer.SetFloat("MyExposedParam", 0);
        MonsterMovement.Instance.mouseValue = 5;
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
    public void AudioAdjust()
    {
        audioSlider.value = Mathf.Clamp(audioSlider.value, -35, 0);
        if (audioSlider.value == -35)
        {
            AudioMixer.SetFloat("MyExposedParam", -40);
        }
        else AudioMixer.SetFloat("MyExposedParam", audioSlider.value);
    }
    public void MouseSenseAdj() 
    {
        mouseSlider.value = Mathf.Clamp(mouseSlider.value, 1, 5);
        MonsterMovement.Instance.mouseValue = mouseSlider.value;
    }
    public void UIUpdate()
    {
       
        hpText.text = $"{monster.CurrentHp / monster.MaxHp * 100}%";
        expText.text = $"{monster.CurrentExp / monster.LevelPerExp*100}%";
        hpBar.fillAmount = monster.CurrentHp / monster.MaxHp;
        expBar.fillAmount = monster.CurrentExp / monster.LevelPerExp;
    }
    public void FindEnemy()
    {
        skillImage.color = Color.red;
    }
    public void LevelUpEffect(int level)
    {
        LvText.text = $"<size=50%>LV</size>\n <color=#770000>{level}";
        LvText.DOFade(1, 3).OnComplete(() => LvText.DOFade(0, 3));
    }
}