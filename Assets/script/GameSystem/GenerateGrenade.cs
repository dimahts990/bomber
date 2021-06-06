using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGrenade : MonoBehaviour
{
    public Transform worldBorderXZ, worldBorderMinusXZ;
    public int MinReSpawnQuantityGrenade;
    public int MaxRespawnQuantityGrenade;
    public float timerReSpawnGrenade;
    public GameObject[] Grenade;
    
    private void OnEnable()
    {
        int quantityReSpawnNPC = Random.Range(MinReSpawnQuantityGrenade, MaxRespawnQuantityGrenade);
        for (int i = 0; i < quantityReSpawnNPC; i++)
        {
            float spawnPositionX = Random.Range(worldBorderMinusXZ.position.x, worldBorderXZ.position.x);
            float spawnPositionZ = Random.Range(worldBorderMinusXZ.position.z, worldBorderXZ.position.z);
            Vector3 spawnPosition = new Vector3(spawnPositionX, 0.2f, spawnPositionZ);
            int grenadeType = Random.Range(0, Grenade.Length);
            Instantiate(Grenade[grenadeType], spawnPosition, Quaternion.identity);
        }
    }

    public void GenerateNewGrenate(Vector3 position)
    {
        StartCoroutine(SpawnNewGrenate(position));
    }

    private IEnumerator SpawnNewGrenate(Vector3 pos)
    {
        yield return new WaitForSeconds(timerReSpawnGrenade);
        
        int grenadeType = Random.Range(0, Grenade.Length);
        Instantiate(Grenade[grenadeType], pos, Quaternion.identity);
    }

}
