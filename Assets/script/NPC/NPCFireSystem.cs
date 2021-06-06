using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFireSystem : MonoBehaviour
{
    [Range(1f,10f)] [SerializeField] private float radiusFire;
    [SerializeField] private Transform spawnPointTransform;
    [SerializeField] private GameObject grenadeGameObject;
    [SerializeField] private Transform handTransform;
    [SerializeField] private float AngleFloat=45f;
    [SerializeField] private float timeToReload;
    
    private float g = Physics.gravity.y;
    private bool shotReadyBool = true;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Update()
    {
        handTransform.localEulerAngles = new Vector3(AngleFloat,0, 0);

        if (player!=null && 
            Vector3.Distance(player.transform.position, transform.position) <= radiusFire)
        {
            #region предварительные расчёты траектории
            
            Vector3 fromTo = player.transform.position - spawnPointTransform.position;
            Vector3 fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);

            transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

            float x = fromToXZ.magnitude;
            float y = fromTo.y;

            float AngleInRadians = AngleFloat * Mathf.PI / 180;

            float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
            float vShot = Mathf.Sqrt(Mathf.Abs(v2));
            
            #endregion
            
            if(shotReadyBool)
                StartCoroutine(Fire(player.transform.position,vShot));
        }
    }

    private IEnumerator Fire(Vector3 posTarget,float vShot)
    {
        shotReadyBool = false;
        yield return new WaitForSeconds(3);
        
        #region выстрел
        
        GameObject _grenade = Instantiate(grenadeGameObject, spawnPointTransform.position, Quaternion.identity);
        _grenade.transform.SetParent(null);
        _grenade.GetComponent<Rigidbody>().velocity = spawnPointTransform.forward * vShot;
        
        #endregion

        yield return new WaitForSeconds(timeToReload);
        shotReadyBool = true;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireSphere(transform.position,radiusFire);
    }

}
