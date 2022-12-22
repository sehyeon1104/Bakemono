    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MonsterInput : MonoSingleton<MonsterInput>
{
    [Header("���Ͱ� �Է¹޴� ������ �ִ� ��ũ��Ʈ")]

    [SerializeField] UnityEvent<Vector3> moveKeyPress;
    [SerializeField] UnityEvent monsterSkill;
    [SerializeField] UnityEvent<float> rotateMouse;
    [SerializeField] UnityEvent OpenDoor;
    [SerializeField] private GameObject doorLock = null;
    [SerializeField] private float doorLockDis = 5f;
    [SerializeField] private GameObject healText;

    private LayerMask bedLayer;
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
            // ������ �ݱ�
            if(Vector3.Distance(doorLock.transform.position, transform.position) < doorLockDis && !PasswordManager.Instance.isSucceed)
            {
                UIManager.Instance.TogglePasswordPanel(!UIManager.Instance.passwordPanel.activeSelf);
            }
        }
        healText.SetActive(false);
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.5f, transform.forward, out hit, 1f, bedLayer))
        {
            healText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                // ġ��� ȸ��
                GameManager.Instance.HealMonster();
            }
        }

    }
    
}
