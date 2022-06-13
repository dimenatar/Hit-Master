using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyCounterView : MonoBehaviour
{
    [SerializeField] private EnemyCounter _enemyCounter;
    [SerializeField] private Slider _slider;

    [SerializeField] private float _sliderAnimationDuration;

    private void Awake()
    {
        _enemyCounter.OnCounterChanged += UpdateSlider;
    }

    private void UpdateSlider(int count)
    {
        _slider.DOValue(1 - (float)count / _enemyCounter.StartAmount, _sliderAnimationDuration);
    }
}
