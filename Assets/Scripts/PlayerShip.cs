using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerShip : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 12f;
    [SerializeField] float _turnSpeed = 3f;

    Rigidbody _rb = null;
    ParticleSystem _ps;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _ps = GetComponentInChildren<ParticleSystem>();
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
    }

    //Use forces to build momentum forward/backward
    void MoveShip()
    {
        //S/Down = -1, W/Up  1, None = 0. Scale by moveSpeed
        float moveAmountThisFrame = Input.GetAxisRaw("Vertical") * _moveSpeed;
        // combine our direction with our calculated amount
        Vector3 moveDirection = transform.forward * moveAmountThisFrame;
        // apply the movement to the physics object
        _rb.AddForce(moveDirection);

        if (_moveSpeed != null)
        {
            _ps.Play();
        } else
        {
            _ps.Stop();
        }
    }

    // don't use forces for this. We want rotations to be precise
    void TurnShip()
    {
        // A/Left = -1, D/Right = 1, None = 0. Scale by turnSpeed
        float turnAmountThisFrame = Input.GetAxisRaw("Horizontal") * _turnSpeed;
        // specify an axis to apply our turn amount (x,y,z) as a rotation
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // spin the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);

        if (_turnSpeed != null)
        {
            _ps.Play();
        } else
        {
            _ps.Stop();
        }
    }

    public void Kill()
    {
        this.gameObject.SetActive(false);
    }
}
