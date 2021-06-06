using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messeng : MonoBehaviour
{
    [SerializeField] private float speed=0.5f;
    
    private TextMesh _textMesh;

    private void OnEnable()
    {
        _textMesh = GetComponent<TextMesh>();
    }

    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        if (_textMesh.color.a <= 0)
            Destroy(gameObject);
        else
            _textMesh.color -= new Color(0, 0, 0, Time.deltaTime * speed);
    }

    public void WriteInfo(string messeng)
    {
        _textMesh.text = messeng;
    }
}
