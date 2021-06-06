using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrenade : MonoBehaviour
{
    public Transform worldBorderXZ, worldBorderMinusXZ;
    public int MaxReSpawnQuantityGrenade;
    public float timerReSpawnGrenade;
    public GameObject[] Grenade;
    
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    public void GenerateNewGrenate(Vector3 position)
    {
        StartCoroutine(SpawnNewGrenate(position));
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
    }

    private IEnumerator SpawnNewGrenate(Vector3 pos)
    {
        yield return new WaitForSeconds(timerReSpawnGrenade);
        
        int grenadeType = Random.Range(0, Grenade.Length);
        Instantiate(Grenade[grenadeType], pos, Quaternion.identity);
    }

}
