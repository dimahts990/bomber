using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabGrenade : MonoBehaviour
{
    [Range (1f,3f)]
    public float Radius;
    SphereCollider grabCollider;

    private void Awake()
    {
        grabCollider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        grabCollider.radius = Radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Grenade>() && other.GetComponent<Grenade>().GrabTrue)
        {
            transform.parent.GetComponent<PlayerManager>().Inventory.AddGrenade(other.gameObject);
        }
    }
}
