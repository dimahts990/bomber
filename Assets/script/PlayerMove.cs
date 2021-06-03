using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float MoveSpeed;
    Vector3 inputAxis;

    private void Update()
    {
        inputAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (inputAxis != Vector3.zero)
            move(inputAxis);
    }

    void move(Vector3 _inputAxis)
    {
        transform.Translate(_inputAxis * MoveSpeed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))), 5 * Time.deltaTime);
    }
}
