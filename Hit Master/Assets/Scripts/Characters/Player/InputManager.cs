using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action OnFirstTouch;
    public event Action<Vector3> OnTouch;

    private bool _isFirstTouch;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isFirstTouch)
            {
                _isFirstTouch = true;
                OnFirstTouch?.Invoke();
            }
            OnTouch?.Invoke(Input.mousePosition);
        }
        //if (Input.touchCount > 0)
        //{
        //    OnTouch?.Invoke(Input.GetTouch(0).position);
        //}
    }
}
