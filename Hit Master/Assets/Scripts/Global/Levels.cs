using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public event Action<int> OnLevelChanged;

    public void Initialise(int level)
    {
        _currentLevel = level;
        OnLevelChanged?.Invoke(level);
    }
}
