using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    [SerializeField] private int HP;
    [SerializeField] private GameObject messengGameObject;

    public void DamageMe(int damage)
    {
        HP -= damage;
        GameObject mes = Instantiate(messengGameObject, transform.position, Quaternion.identity);
        mes.GetComponent<Messeng>().WriteInfo(damage.ToString());
        mes.transform.SetParent(null);
        if(HP<=0)
            Destroy(gameObject);
    }
}
