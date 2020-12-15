using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _myRigidbody;
    private Vector3 _velocity;

    private void Start()
    {
        _myRigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
    }

    public void LookAt(Vector3 lookPoint)
    {
        var heightCorrectedPoint=  new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorrectedPoint);
    }

    private void FixedUpdate()
    {
        _myRigidbody.MovePosition(_myRigidbody.position + _velocity * Time.fixedDeltaTime);
    }
}