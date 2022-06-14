using System;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private bool _isFirstPlatform;
    [SerializeField] private bool _isFinish;
    [SerializeField] private Transform _startPlayerPosition;

    private int _enemyCount;

    public int EnemyCount => _enemyCount;
    public Vector3 StartPlayerPosition => _startPlayerPosition.position;

    public event Action OnPlayerEntersPlatform;
    public event Action<Platform> OnPlatformCleared;

    private void Awake()
    {
        _enemyCount = _enemies.Count;
    }

    private void Start()
    {
        _enemies.ForEach(enemy => OnPlayerEntersPlatform += enemy.Enable);
        _enemies.ForEach(enemy => enemy.OnDied += () => _enemies.Remove(enemy));
        _enemies.ForEach(enemy => enemy.OnDied += ReduceEnemy);
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
        return point;
    }

    public Transform GetClosestEnemy(Transform relative)
    {
        if (_enemies.Count == 0) return null;
        Transform point;
        point = _enemies[0].transform;
        float min = Vector3.Distance(point.position, relative.position);
        for (int i = 1; i < _enemies.Count; i++)
        {
            float distance = Vector3.Distance(_enemies[i].transform.position, relative.position);
            if (min > distance)
            {
                point = _enemies[i].transform;
                min = distance;
            }
        }
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
    }

    public void SubscribeEnemiesOnDied(Action action)
    {
        _enemies.ForEach(enemy => enemy.OnDied += action);
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
