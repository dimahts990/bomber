using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrenade : MonoBehaviour
{
    public float timerReSpawnGrenade;
    public int MaxReSpawnQuantityGrenade;
    public GameObject[] Grenade;
    public Transform worldBorderXZ, worldBorderMinusXZ;
    
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        int quantityReSpawnNPC = Random.Range(1, MaxReSpawnQuantityGrenade);
        for (int i = 0; i < quantityReSpawnNPC; i++)
        {
            float spawnPositionX = Random.Range(worldBorderMinusXZ.position.x, worldBorderXZ.position.x);
            float spawnPositionZ = Random.Range(worldBorderMinusXZ.position.z, worldBorderXZ.position.z);
            Vector3 spawnPosition = new Vector3(spawnPositionX, 0.2f, spawnPositionZ);
            int grenadeType = Random.Range(0, Grenade.Length);
            Instantiate(Grenade[grenadeType], spawnPosition, Quaternion.identity);
        }
        yield return new WaitForSeconds(timerReSpawnGrenade);
        StartCoroutine(Spawn());
    }
}
