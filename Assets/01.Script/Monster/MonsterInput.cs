    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MonsterInput : MonoSingleton<MonsterInput>
{
    [Header("몬스터가 입력받는 모든것을 넣는 스크립트")]

    [SerializeField] UnityEvent<Vector3> moveKeyPress;
    [SerializeField] UnityEvent monsterSkill;
    [SerializeField] UnityEvent<float> rotateMouse;
    [SerializeField] UnityEvent OpenDoor;
    [SerializeField] private GameObject doorLock = null;
    [SerializeField] private float doorLockDis = 5f;
    [SerializeField] private GameObject healText;

    private LayerMask bedLayer;
    private float healCooltime = 180;
    public float runValue;

    private void Awake()
    {
        bedLayer = 1 << 10;
    }
    [SerializeField]
    private void Update()
    {
       
        if (UIManager.Instance.isPause || Monster.Instance.isDie)
        {
            return;
        }
        if (UIManager.Instance.passwordPanel.activeSelf)
        {
            if (Vector3.Distance(doorLock.transform.position, transform.position) > doorLockDis)
            {
                UIManager.Instance.TogglePasswordPanel(false);
            }
        }
        if(Input.GetKey(KeyCode.LeftShift))
        {
            runValue = 2;
        }
        else
        {
            runValue = 1;
        }

        if(healCooltime <= 60f)
            healCooltime += Time.deltaTime;

        MonsterMove();
        MonsterRotate();
        InputKey();
    }
    public void MonsterRotate()
    {
        rotateMouse?.Invoke(Input.GetAxisRaw("Mouse X"));
    }
   public void MonsterMove()
    {   
        moveKeyPress?.Invoke(new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"))*runValue);
    }
    public void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // 문열고 닫기
            if(Vector3.Distance(doorLock.transform.position, transform.position) < doorLockDis && !PasswordManager.Instance.isSucceed)
            {
                UIManager.Instance.TogglePasswordPanel(!UIManager.Instance.passwordPanel.activeSelf);

            }
        }
        healText.SetActive(false);

        if (healCooltime >= 60f && Monster.Instance.CurrentHp < Monster.Instance.MaxHp)
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, 1f, bedLayer))
            {
                healText.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    // 치료실 회복
                    healCooltime = 0f;
                    GameManager.Instance.HealMonster();
                }
            }
        }

    }
    
}
