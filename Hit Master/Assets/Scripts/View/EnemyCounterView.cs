using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCounterView : MonoBehaviour
{
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private Slider _slider;

    private void Awake()
    {
        _enemyCounter.OnCounterChanged += UpdateSlider;
    }

    private void UpdateSlider(int count)
    {
        _slider.value = 1 - (float)count / _enemyCounter.StartAmount;
    }
}
