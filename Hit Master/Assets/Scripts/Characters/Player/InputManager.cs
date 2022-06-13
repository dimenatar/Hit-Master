using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public event Action OnFirstTouch;
    public event Action OnTouch;

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
            OnTouch?.Invoke();
        }
        //if (Input.touchCount > 0)
        //{
        //    OnTouch?.Invoke(Input.GetTouch(0).position);
        //}
    }
}
