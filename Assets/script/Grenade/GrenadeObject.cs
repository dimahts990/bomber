using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeObject : MonoBehaviour
{
    [Header("Если граната для NPC, поставить флаг")] [SerializeField]
    private bool ForNPC;
    
    [Space] [SerializeField]
    private GameObject radiusDamageGameObject;
    public AssetGrenade _assetGrenade;
    public GameObject BoomParticalSystem;
    public bool ReadyToGrab = true;
    public int Damage;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != 2)
        {
            switch (ForNPC)
            {
                case true:
                    if (!ReadyToGrab && other.gameObject.layer != 9)
                    {
                        Instantiate(BoomParticalSystem, transform.position, Quaternion.identity).transform
                            .SetParent(null);
                        if (other.gameObject.GetComponent<PlayerController>())
                            BoomPlayer(other.gameObject);
                        else
                            radiusDamageGameObject.SetActive(true);
                    }

                    break;
                case false:
                    if (!ReadyToGrab && other.gameObject.layer != 8)
                    {
                        Instantiate(BoomParticalSystem, transform.position, Quaternion.identity).transform
                            .SetParent(null);
                        if (other.gameObject.GetComponent<NPCController>())
                            BoomNPC(other.gameObject);
                        else
                            radiusDamageGameObject.SetActive(true);
                    }

                    break;
            }
        }
    }

    public void BoomNPC(GameObject target=null)
    {
        if(target!=null)
            target.GetComponent<NPCController>().DamageMe(Damage);
        
        Destroy(gameObject);
    }
    
    public void BoomPlayer(GameObject target=null)
    {
        if(target!=null)
            target.GetComponent<PlayerController>().DamageMe(Damage);
        
        Destroy(gameObject);
    }
}
