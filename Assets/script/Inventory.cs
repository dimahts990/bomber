using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> Grenades = new List<GameObject>();

    public void AddGrenade(GameObject grenade)
    {
        Grenades.Add(grenade);
        grenade.SetActive(false);
        grenade.transform.SetParent(transform);
    }

    public void DestroyGrenade(GameObject grenade)
    {
        grenade.SetActive(true);
        grenade.transform.SetParent(null);
        Grenades.Remove(grenade);
    }
}
