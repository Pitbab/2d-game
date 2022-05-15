using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBrain : MonoBehaviour
{
    #region Variables

    public float horAxis { get; private set; }
    public bool jumping { get; private set; }
    public bool blocking { get; private set; }
    public bool rolling { get; private set; }

    #endregion

    #region Events

    private void Update()
    {
        horAxis = Input.GetAxis("Horizontal");
        jumping = Input.GetButtonDown("Jump");
        rolling = Input.GetButtonDown("Fire3");
        blocking = Input.GetButton("Fire2");
    }

    #endregion
}
