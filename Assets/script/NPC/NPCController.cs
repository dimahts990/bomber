using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class NPCController : MonoBehaviour
{
    [SerializeField] private GameObject messengGameObject;
    [SerializeField] private int HP;
    private GenerateNPC _generateNpc;

    private void Start()
    {
        _generateNpc = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GenerateNPC>();
    }
    
    public void DamageMe(int damage)
    {
        HP -= damage;
        GameObject mes = Instantiate(messengGameObject, transform.position, Quaternion.identity);
        mes.GetComponent<Messeng>().WriteInfo(damage.ToString());
        mes.transform.SetParent(null);
        if (HP <= 0)
        {
            _generateNpc.GenerateNewNPC(transform.position);
            Destroy(gameObject);
        }
    }
}
