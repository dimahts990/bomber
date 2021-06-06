using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [SerializeField] private Transform handTransform, spawnPointTransform, viewInventoryTransform;
    [SerializeField] private LineRenderer trajectoryLineRenderer;
    [SerializeField] private PlayerMove _playerMove;
    
    public GameObject ActiveGrenadeGameObject;
    public float AngleFloat;
    public int SizePointsInLineTrajectory = 100;

    private float g = Physics.gravity.y, vShot;
    private Vector3 fromTo, fromToXZ;

    private void Update()
    {
        handTransform.localEulerAngles = new Vector3(AngleFloat,0, 0);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (viewInventoryTransform.childCount > 0)
        {
            if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
                takeAim(hit.point);

            if (Input.GetMouseButtonUp(0) && !_playerMove.enabled)
                Shot();
        }
    }
    
    private void takeAim(Vector3 mousePosition)
    {
        _playerMove.enabled = false;

        #region предварительные расчёты траектории
        fromTo = mousePosition - spawnPointTransform.position;
        fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float AngleInRadians = AngleFloat * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        vShot = Mathf.Sqrt(Mathf.Abs(v2));
        #endregion

        showTrajectory(spawnPointTransform.position, spawnPointTransform.forward * vShot);
    }
    
    private void Shot()
    {
        GameObject _grenade = Instantiate(ActiveGrenadeGameObject, spawnPointTransform.position, Quaternion.identity);
        _grenade.transform.SetParent(null);
        _grenade.GetComponent<Rigidbody>().velocity = spawnPointTransform.forward * vShot;
        
        viewInventoryTransform.GetComponent<Inventory>().RemoveFromInventory();
        _playerMove.enabled = true;
        clearTrajectory();
    }
    
    private void showTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[SizePointsInLineTrajectory];

        for (int i = 0; i < SizePointsInLineTrajectory; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + speed * time + Physics.gravity * time * time / 2f;
            if (points[i].y < 0)
            {
                trajectoryLineRenderer.positionCount = i + 1;
                break;
            }
        }
        trajectoryLineRenderer.SetPositions(points);
    }

    private void clearTrajectory() => trajectoryLineRenderer.positionCount = 0;
}
