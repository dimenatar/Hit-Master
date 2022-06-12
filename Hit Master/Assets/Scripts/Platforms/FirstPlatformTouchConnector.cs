using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlatformTouchConnector : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    [SerializeField] private Platform _firstPlatform;

    private void Awake()
    {
        _input.OnTouch += _firstPlatform.Initialise;
    }
}
