using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class playerAttackSystem : MonoBehaviour
{
    [SerializeField] private Transform handTransform, spawnBulletTransform, viewInventory;
    [SerializeField] private LineRenderer grenadeTrajectory;
    [SerializeField] private PlayerMove PlayerMove;

    public GameObject ActiveGrenadeGameObject;
    public int SizePointsInLineTrajectory = 100;
    private float g = Physics.gravity.y, vShot;
    private Vector3 fromTo, fromToXZ;
    private bool shotReady = false;
    public float Angle;
    
    private void Update()
    {
        handTransform.localEulerAngles = new Vector3(Angle, 0f, 0f);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (viewInventory.childCount != 0)
        {
            if (Input.GetMouseButton(0) && Physics.Raycast(ray, out hit))
                takeAim(hit.point);

            if (Input.GetMouseButtonUp(0) && shotReady)
                Shot();
        }
    }

    void takeAim(Vector3 mousePosition)
    {
        shotReady = true;
        PlayerMove.enabled = false;

        #region предварительные расчёты траектории
        fromTo = mousePosition - spawnBulletTransform.position;
        fromToXZ = new Vector3(fromTo.x, 0, fromTo.z);

        transform.rotation = Quaternion.LookRotation(fromToXZ, Vector3.up);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float AngleInRadians = Angle * Mathf.PI / 180;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(AngleInRadians) * x) * Mathf.Pow(Mathf.Cos(AngleInRadians), 2));
        vShot = Mathf.Sqrt(Mathf.Abs(v2));
        #endregion

        showTrajectory(spawnBulletTransform.position, spawnBulletTransform.forward * vShot);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    void Shot()
    {
        if (ActiveGrenadeGameObject != null)
        {
            ActiveGrenadeGameObject.transform.position = spawnBulletTransform.position;
            ActiveGrenadeGameObject.transform.SetParent(null);
            ActiveGrenadeGameObject.SetActive(true);
            ActiveGrenadeGameObject.GetComponent<Rigidbody>().velocity = spawnBulletTransform.forward * vShot;
            viewInventory.GetChild(viewInventory.GetComponent<Inventory>().SelectedActiveInt).GetComponent<InventoryCell>().Inject();
            ActiveGrenadeGameObject = null;
        }
        else Debug.Log("Active Grenade GameObject is null");
        shotReady = false;
        PlayerMove.enabled = true;
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
                grenadeTrajectory.positionCount = i + 1;
                break;
            }
        }
        grenadeTrajectory.SetPositions(points);
    }

    private void clearTrajectory() => grenadeTrajectory.positionCount = 0;
}

