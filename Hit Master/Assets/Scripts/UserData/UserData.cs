using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserData : MonoBehaviour
{
    [SerializeField] private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public void SaveData(int level)
    {
        _currentLevel = level;
    }
}
