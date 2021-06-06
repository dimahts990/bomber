using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeObject : MonoBehaviour
{
    [SerializeField] private GameObject radiusDamageGameObject;
    public AssetGrenade _assetGrenade;
    public GameObject BoomParticalSystem;
    public bool ReadyToGrab = true;
    public int Damage;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!ReadyToGrab && other.gameObject.layer!=8)
        {
            Instantiate(BoomParticalSystem, transform.position, Quaternion.identity).transform.SetParent(null);
            if (other.gameObject.GetComponent<NPCController>())
                Boom(other.gameObject);
            else
                radiusDamageGameObject.SetActive(true);
        }
    }

    public void Boom(GameObject target=null)
    {
        if(target!=null)
            target.GetComponent<NPCController>().DamageMe(Damage);
        
        Destroy(gameObject);

    }
    
    //TODO:сделать взрыв (в AssetGrenade есть параметр _DamageRadius)
}
