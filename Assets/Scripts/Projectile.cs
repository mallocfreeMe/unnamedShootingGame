using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float _speed = 10;

    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
    
    public void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    }
}
