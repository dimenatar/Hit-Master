using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnabler : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Platforms _platforms;
    [SerializeField] private Transform _player;
    [SerializeField] private float _delayToShow = 6f;
    [SerializeField] private Target _firstEnemy;

    private float _timer;

    private Target _currentEnabledTarget;

    private void Awake()
    {
        _currentEnabledTarget = _firstEnemy;
        _inputManager.OnTouch += Touch;
    }

    private IEnumerator CheckTarget()
    {
        _timer = 0;
        while (_timer < _delayToShow)
        {
            _timer += Time.deltaTime;
            yield return null;
        }
        ShowTargetOnClosestEnemy();
    }

    private void Touch()
    {
        if (_currentEnabledTarget)
        {
            _currentEnabledTarget.HideTarget();
        }
        _currentEnabledTarget = null;
        StopAllCoroutines();
        StartCoroutine(CheckTarget());
    }

    private void ShowTargetOnClosestEnemy()
    {
        if (!_currentEnabledTarget)
        {
            var closestEnemy = _platforms.CurrentPlatform.GetClosestEnemy(_player);
            if (closestEnemy)
            {
                _currentEnabledTarget = closestEnemy.GetComponent<Target>();
                _currentEnabledTarget.ShowTarget();
                _timer = 0;
            }
        }
    }
}
