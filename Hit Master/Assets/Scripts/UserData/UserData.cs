using UnityEngine;

[System.Serializable]
public class UserData
{
    [SerializeField] private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public UserData()
    {
        _currentLevel = 1;
    }

    public void SaveData(int level)
    {
        _currentLevel = level;
    }
}
