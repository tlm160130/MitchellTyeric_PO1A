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

    [Header("Feedback")]
    [SerializeField] TrailRenderer _trail = null;

    Rigidbody _rb = null;
    ParticleSystem _ps;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _ps = GetComponentInChildren<ParticleSystem>();
        _trail.enabled = false;
    }

    private void FixedUpdate()
    {
        MoveShip();
        TurnShip();
        ThrustersOn();

        //This will turn the Thrusters on or off when the player presses the spacebar
        void ThrustersOn()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _ps.Play();
            }

            if (Input.GetButtonUp("Fire2"))
            {
                _ps.Stop();
            }
        }
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
    }

    public void Kill()
    {
        this.gameObject.SetActive(false);
    }

    public void SetSpeed(float SpeedChange)
    {
        _moveSpeed += SpeedChange;
        //TODO audio/visuals
    }

    public void SetBoosters(bool activeState)
    {
        _trail.enabled = activeState;
    }
}
