using System;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private Platform _finishPlatform;

    public event Action OnFinish;

    private void Awake()
    {
        _finishPlatform.OnPlayerEntersPlatform += () => OnFinish?.Invoke();
    }
}
