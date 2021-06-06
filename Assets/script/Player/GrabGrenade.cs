using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GrabGrenade : MonoBehaviour
{
    [SerializeField] private Inventory _inventory;
    [Range(1f, 3f)] public float Radius;
    SphereCollider grabCollider;

    private GenerateGrenade _generateGrenade;

    private void Awake()
    {
        grabCollider = GetComponent<SphereCollider>();
        _generateGrenade = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GenerateGrenade>();

    }

    void Update()
    {
        grabCollider.radius = Radius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GrenadeObject>() && other.GetComponent<GrenadeObject>().ReadyToGrab)
        {
            _generateGrenade.GenerateNewGrenate(other.transform.position);
            other.GetComponent<GrenadeObject>().ReadyToGrab = false;
            _inventory.AddInventory(other.GetComponent<GrenadeObject>()._assetGrenade);
            Destroy(other.gameObject);
        }
    }
}
