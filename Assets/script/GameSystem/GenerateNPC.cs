using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateNPC : MonoBehaviour
{
    public float timerReSpawnNPC;
    public int MaxReSpawnQuantityNPC;
    public GameObject NPC;
    public Transform worldBorderXZ, worldBorderMinusXZ;
    
    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        int quantityReSpawnNPC = Random.Range(1, MaxReSpawnQuantityNPC);
        for (int i = 0; i < quantityReSpawnNPC; i++)
        {
            float spawnPositionX = Random.Range(worldBorderMinusXZ.position.x, worldBorderXZ.position.x);
            float spawnPositionZ = Random.Range(worldBorderMinusXZ.position.z, worldBorderXZ.position.z);
            Vector3 spawnPosition = new Vector3(spawnPositionX, 0.5f, spawnPositionZ);
            
            Instantiate(NPC, spawnPosition, Quaternion.identity);
        }
        yield return new WaitForSeconds(timerReSpawnNPC);
        StartCoroutine(Spawn());
    }
}
