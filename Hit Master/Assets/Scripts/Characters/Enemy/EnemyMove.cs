using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _delayToStartMove = 1;

    private Transform _player;
    private bool _isInitialised;

    public void Initialise(Transform player)
    {
        _player = player;
    }

    public void StartMove()
    {
        StartCoroutine(Move());
    }

    public void StopMove()
    {
        _agent.enabled = false;
    }

    private void OnDestroy()
    {
        _agent.enabled = false;
        StopAllCoroutines();
    }

    private IEnumerator Move()
    {
        yield return new WaitForSeconds(_delayToStartMove);
        _isInitialised = true;
        _agent.enabled = true;
        _agent.SetDestination(_player.position);
    }
}
