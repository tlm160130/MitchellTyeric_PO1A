﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform _objectToFollow = null;

    Vector3 _objectOffset;

    private void Awake()
    {
        // create an offset between this position and object's position
        _objectOffset = this.transform.position - _objectToFollow.position;
    }

    // happens after Update. Camera should always move last
    private void LateUpdate()
    {
        // apply the offset every frame, to repositon this object
        this.transform.position = _objectToFollow.position + _objectOffset;
    }
}
