using UnityEngine;

public class FirstPlatformTouchConnector : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    [SerializeField] private Platform _firstPlatform;

    private void Awake()
    {
        _input.OnFirstTouch += _firstPlatform.Initialise;
    }
}
