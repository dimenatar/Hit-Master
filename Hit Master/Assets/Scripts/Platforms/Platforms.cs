using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platforms : MonoBehaviour
{
    [SerializeField] private List<Platform> _platforms;
    [SerializeField] private Platform _finishPlatform;
    [SerializeField] private Transform _player;

    private Platform _currentPlatform;

    public Platform CurrentPlatform => _currentPlatform;
    public List<Platform> PlatformList => _platforms;

    public event Action<Platform> OnCurrentPlatformChanged;

    private void Awake()
    {
        _currentPlatform = _platforms[0];
    }

    private void Start()
    {
        _platforms.ForEach(platform => platform.InitialiseEnemies(_player));
        _platforms.ForEach(platform => platform.OnPlatformCleared += PlatformCleared);
    }

    public bool IsLastPlatform(Platform platform) => _platforms.IndexOf(platform) == _platforms.Count - 1;

    public Platform GetNextPlatform()
    {
        if (IsLastPlatform(_currentPlatform))
        {
            return _finishPlatform;
        }
        else
        {
            return _platforms[_platforms.IndexOf(_currentPlatform) + 1];
        }
    }

    public void PlatformCleared(Platform platform)
    {
        _currentPlatform = GetNextPlatform();
        print(_currentPlatform);
        OnCurrentPlatformChanged?.Invoke(_currentPlatform);
    }
}
