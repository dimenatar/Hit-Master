using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroSceneLoader : MonoBehaviour
{
    [SerializeField] private DataManager _dataManager;
    [SerializeField] private LevelLoader _levelLoader;

    private readonly Dictionary<int, string> _scenes = new Dictionary<int, string> { { 1, "FirstLevel" }, { 2, "SecondLevel" } };

    public void Play()
    {
        print("1");
        int level = _dataManager.UserData.CurrentLevel;
        _levelLoader.LoadLevel(_scenes[level]);
    }
}
