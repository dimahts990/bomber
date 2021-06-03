using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttackSystem : MonoBehaviour
{
    public GameObject Bullet;
    public float Angle;
    public int SizePointsInLineTrajectory = 100;

    PlayerMove PlayerMove;
    LineRenderer bulletTrajectory;

    Transform handTransform, spawnBulletTransform;
    Vector3 fromTo, fromToXZ;

    float g = Physics.gravity.y, vShot;
    bool shotReady = false;

    private void Awake()
    {
        handTransform = transform.GetChild(0);
        spawnBulletTransform = handTransform.GetChild(0);
        bulletTrajectory = spawnBulletTransform.GetComponent<LineRenderer>();
        PlayerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        handTransform.localEulerAngles = new Vector3(Angle, 0f, 0f);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (GetComponent<PlayerManager>().Inventory.Grenades.Count > 0)
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

    void Shot()
    {
        GameObject newBullet = Instantiate(Bullet, spawnBulletTransform.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody>().velocity = spawnBulletTransform.forward * vShot;
        
        shotReady = false;
        PlayerMove.enabled = true;
    }

    void showTrajectory(Vector3 origin, Vector3 speed)
    {
        Vector3[] points = new Vector3[SizePointsInLineTrajectory];

        for (int i = 0; i < SizePointsInLineTrajectory; i++)
        {
            float time = i * 0.1f;
            points[i] = origin + speed * time + Physics.gravity * time * time / 2f;
            if (points[i].y < 0)
            {
                bulletTrajectory.positionCount = i + 1;
                break;
            }
        }
        bulletTrajectory.SetPositions(points);
    }
}

