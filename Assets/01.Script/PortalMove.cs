using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalMove : MonoBehaviour
{
    Transform monster;
    private void Update()
    {
      
    }
    private void Awake()
    {
        monster = GameObject.FindGameObjectWithTag("Monster").transform;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Monster"))
        {

            var cc = monster.GetComponent<CharacterController>();
            cc.enabled= false;
            monster.position = new Vector3(90, -18, -3.75f);
            monster.eulerAngles = new Vector3(0,90,0);  
            cc.enabled = true;
        }
    }   
}
