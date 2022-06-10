using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action<Vector3> OnTouch;

    void Update()
    {
        //if (Input.touchCount > 0)
        //{
        //    OnTouch?.Invoke(Input.GetTouch(0).position);
        //}
    }
}
