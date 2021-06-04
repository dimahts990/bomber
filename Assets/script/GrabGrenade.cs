using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabGrenade : MonoBehaviour
{
    [Range (1f,3f)]
    public float Radius;
    SphereCollider grabCollider;

    [SerializeField]
    private Inventory _inventory;

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
        if (other.GetComponent<GrenadeObject>() && other.GetComponent<GrenadeObject>().ReadyToGrab)
        {
            other.GetComponent<GrenadeObject>().ReadyToGrab = false;
            other.gameObject.SetActive(false);
            other.transform.SetParent(transform);
            _inventory.AddInventory(other.GetComponent<GrenadeObject>()._assetGrenade, other.gameObject);
        }
    }
}
