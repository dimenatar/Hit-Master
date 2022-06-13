using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private int _maxLevels;

    private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public event Action<int> OnLevelChanged;

    private void Awake()
    {
        _dataManager.OnDataLoaded += () => Initialise(_dataManager.UserData.CurrentLevel);   
    }

    public void UpdateCurrentLevel()
    {
        if (_currentLevel < _maxLevels)
        {
            _currentLevel++;
        }
        else
        {
            _currentLevel = 1;
        }
        _dataManager.SaveData(_currentLevel);
    }

    private void Initialise(int level)
    {
        _currentLevel = level;
        OnLevelChanged?.Invoke(level);
    }
}
