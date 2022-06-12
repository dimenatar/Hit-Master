using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    [SerializeField] private Ragdoll _ragdoll;
    [SerializeField] private Animator _animator;
    [SerializeField] private Enemy _enemy;

    private void Awake()
    {
        _enemy.OnEnable += () => _animator.SetTrigger(WALK);
        _enemy.OnPlayerEntersTrigger += () => _animator.SetTrigger(HIT);

        _ragdoll.OnFall += () => _animator.enabled = false;
        _ragdoll.OnStandedUp += () => _animator.enabled = true;
    }

    #region Animator hashes
    private readonly int WALK = Animator.StringToHash("Walk");
    private readonly int HIT = Animator.StringToHash("Hit");
    #endregion

    private void OnDestroy()
    {
        _animator.enabled = false;
    }
}
