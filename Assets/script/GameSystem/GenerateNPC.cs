using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateNPC : MonoBehaviour
{
    public float timerReSpawnNPC;
    public int MinRespawnQuantityNPC;
    public int MaxRespawnQuantityNPC;
    public GameObject NPC;
    public Transform worldBorderXZ, worldBorderMinusXZ;
    
    private void OnEnable()
    {
        int quantityReSpawnNPC = Random.Range(MinRespawnQuantityNPC, MaxRespawnQuantityNPC);
        for (int i = 0; i < quantityReSpawnNPC; i++)
        {
            float spawnPositionX = Random.Range(worldBorderMinusXZ.position.x, worldBorderXZ.position.x);
            float spawnPositionZ = Random.Range(worldBorderMinusXZ.position.z, worldBorderXZ.position.z);
            Vector3 spawnPosition = new Vector3(spawnPositionX, 0.5f, spawnPositionZ);
            Instantiate(NPC, spawnPosition, Quaternion.identity);
        }
    }

    public void GenerateNewNPC(Vector3 position)
    {
        StartCoroutine(SpawnNewNPC(position));
    }
    
    private IEnumerator SpawnNewNPC(Vector3 pos)
    {
        yield return new WaitForSeconds(timerReSpawnNPC);

        Instantiate(NPC, pos, Quaternion.identity);
    }
    
}
