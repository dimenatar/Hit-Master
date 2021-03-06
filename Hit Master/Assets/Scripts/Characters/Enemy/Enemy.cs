using System;
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
    public event Action<Vector3> OnHit;

    private void Awake()
    {
        _health = _startHealth;

        _body.OnHit += GetHit;
        _head.OnHit += GetSpecialHit;
        _head2.OnHit += GetSpecialHit;

        _enemyMove = GetComponent<EnemyMove>();
        _enemyAnimations = GetComponent<EnemyAnimations>();

        OnEnable += _enemyMove.StartMove;

        OnDied += Destroy;
        OnDied += _ragdoll.FullyFall;

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
        TakeDamage();
        ApplyHit(position, knife);
    }

    public void GetSpecialHit(Vector3 position, GameObject knife)
    {
        TakeDamage(_startHealth);
        ApplyHit(position, knife);
    }

    public void Die() => OnDied?.Invoke();

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
        OnHit?.Invoke(position);
        _ragdoll.PunchRigidbody(position);
        knife.transform.SetParent(_parentToKnifes);
    }
}
