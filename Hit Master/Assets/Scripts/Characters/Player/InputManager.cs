using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private float _delayToRecieveTouch = 0.5f;

    public event Action OnFirstTouch;
    public event Action<Vector3> OnTouch;

    private bool _isFirstTouch;
    private float _timer;

    void Update()
    {
        if (_timer >= _delayToRecieveTouch)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!_isFirstTouch)
                {
                    _isFirstTouch = true;
                    OnFirstTouch?.Invoke();
                }
                OnTouch?.Invoke(Input.mousePosition);
                _timer = 0;
            }
        }
        _timer += Time.deltaTime;
        //if (Input.touchCount > 0)
        //{
        //    OnTouch?.Invoke(Input.GetTouch(0).position);
        //}
    }
}
