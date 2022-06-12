using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private bool _isFirstPlatform;
    [SerializeField] private Transform _startPlayerPosition;

    private int _enemyCount;
    private Transform _player;

    public Vector3 StartPlayerPosition => _startPlayerPosition.position;

    public event Action OnPlayerEntersPlatform;
    public event Action<Platform> OnPlatformCleared;

    private void Awake()
    {

    }

    private void Start()
    {
        _enemies.ForEach(enemy => OnPlayerEntersPlatform += enemy.Enable);
        _enemyCount = _enemies.Count;
        _enemies.ForEach(enemy => enemy.OnDied += () => _enemies.Remove(enemy));
        _enemies.ForEach(enemy => enemy.OnDied += ReduceEnemy);
       // _enemies.ForEach(enemy => enemy.OnDied += () => _player.GetComponent<PlayerMove>().RotateTowards(GetClosestEnemy(_player.position), false));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_isFirstPlatform)
        {
            if (other.GetComponent<Player>())
            {
                OnPlayerEntersPlatform?.Invoke();
                other.GetComponent<PlayerMove>().RotateTowards(GetClosestEnemy(other.transform.position));
            }
        }
    }

    public Vector3 GetClosestEnemy(Vector3 relative)
    {
        if (_enemies.Count == 0) return relative;
        Vector3 point;
        point = _enemies[0].transform.position;
        float min = Vector3.Distance(point, relative);
        for (int i = 1; i < _enemies.Count; i++)
        {
            float distance = Vector3.Distance(_enemies[i].transform.position, relative);
            if (min > distance)
            {
                point = _enemies[i].transform.position;
                min = distance;
            }
        }
        print(point);
        return point;
    }

    public void Initialise()
    {
        if (_isFirstPlatform)
        {
            OnPlayerEntersPlatform?.Invoke();
        }
    }

    public void InitialiseEnemies(Transform player)
    {
        _enemies.ForEach(enemy => enemy.Initialise(player));
        _player = player;
    }

    private void ReduceEnemy()
    {
        _enemyCount--;
        if (_enemyCount == 0)
        {
            OnPlatformCleared?.Invoke(this);
        }
    }
}
