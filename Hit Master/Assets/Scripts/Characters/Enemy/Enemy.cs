using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimations))]
[RequireComponent(typeof(EnemyMove))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform _parentToKnifes;
    [SerializeField] private PunchPart _body;
    [SerializeField] private PunchPart _head;
    [SerializeField] private PunchPart _head2;
    [SerializeField] Ragdoll _ragdoll;
    [SerializeField] private int _startHealth;

    private int _health;
    private EnemyMove _enemyMove;
    private EnemyAnimations _enemyAnimations;

    public event Action OnPlayerEntersTrigger;
    public event Action OnDied;
    public event Action OnEnable;

    private void Awake()
    {
        _health = _startHealth;

        _body.OnHit += GetHit;
        _head.OnHit += GetSpecialHit;
        _head2.OnHit += GetSpecialHit;

        _enemyMove = GetComponent<EnemyMove>();
        _enemyAnimations = GetComponent<EnemyAnimations>();

        OnEnable += _enemyMove.StartMove;

        OnDied += _ragdoll.FullyFall;
        OnDied += Destroy;

        _ragdoll.OnStandedUp += _enemyMove.StartMove;
        _ragdoll.OnFall += _enemyMove.StopMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            OnPlayerEntersTrigger?.Invoke();
            other.GetComponent<Player>().GetHit();
        }
    }

    public void Initialise(Transform player)
    {
        _enemyMove.Initialise(player);
    }

    public void Enable()
    {
        OnEnable?.Invoke();
    }


    public void GetHit(Vector3 position, GameObject knife)
    {
        print("GET HIT");
        //_ragdoll.PunchRigidbody(position);
        TakeDamage();
        //knife.transform.SetParent(_parentToKnifes);
        ApplyHit(position, knife);
    }

    public void GetSpecialHit(Vector3 position, GameObject knife)
    {
        //_ragdoll.PunchRigidbody(position);
        TakeDamage(_startHealth);
        //knife.transform.SetParent(_parentToKnifes);
        ApplyHit(position, knife);
    }

    private void TakeDamage(int damage = 1)
    {
        _health -= damage;
        if (_health <= 0)
        {
            OnDied?.Invoke();
        }
    }

    private void Destroy()
    {
        Destroy(this);
        Destroy(_enemyMove);
        Destroy(_enemyAnimations);
    }

    private void ApplyHit(Vector3 position, GameObject knife)
    {
        _ragdoll.PunchRigidbody(position);
        knife.transform.SetParent(_parentToKnifes);
    }
}
