using System;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private Platforms _platforms;

    private int _counter;
    private int _startAmount;

    public event Action<int> OnCounterChanged;

    public int Counter { get => _counter; private set => _counter = value; }
    public int StartAmount => _startAmount;

    private void ReduceEnemyCount()
    {
        Counter--;
        OnCounterChanged?.Invoke(Counter);
    }

    private void Awake()
    {
        _platforms.PlatformList.ForEach(platform => platform.SubscribeEnemiesOnDied(ReduceEnemyCount));
    }

    private void Start()
    {
       _platforms.PlatformList.ForEach(platform => _startAmount += platform.EnemyCount);
        _counter = _startAmount;
    }
}
