using UnityEngine;
using UnityEngine.UI;
public class MonsterUI : MonoBehaviour
{

    Monster monster;
    [SerializeField]
    public Image hpBar;
    [SerializeField]
    public Image AttackImg;
    float hpBarValue;
    // Update is called once per frame
    private void Awake()
    {
        monster = GetComponent<Monster>();
    }
    void Update()
    {
        hpBar.fillAmount = monster.CurrentHp / (float)monster.MaxHp;
    }
}