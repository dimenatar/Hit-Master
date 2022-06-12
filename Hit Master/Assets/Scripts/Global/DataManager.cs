using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private UserData _userData;

    private const string FILE_NAME = "UserData";

    public UserData UserData => _userData;

    public event Action OnDataLoaded;

    void Start()
    {
        if (JsonSaver.IsExistsSave(FILE_NAME))
        {
            _userData = JsonSaver.Load<UserData>(FILE_NAME);
        }
        else
        {
            _userData = new UserData();
        }
        OnDataLoaded?.Invoke();
    }

    public void SaveData(int currentLevel)
    {
        _userData.SaveData(currentLevel);
        JsonSaver.Save(_userData, FILE_NAME);
    }
}
