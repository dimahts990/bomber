using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusFinishingDamage : MonoBehaviour
{
    private SphereCollider _sphereCollider;
    private void OnEnable()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.radius = transform.parent.GetComponent<GrenadeObject>()._assetGrenade.DamageRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NPCController>())
            transform.parent.GetComponent<GrenadeObject>().BoomNPC(other.gameObject);
        else if (other.gameObject.GetComponent<PlayerController>())
            transform.parent.GetComponent<GrenadeObject>().BoomPlayer(other.gameObject);
        else
            transform.parent.GetComponent<GrenadeObject>().BoomNPC();
    }
}
