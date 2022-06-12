using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;

    private Transform _player;

    public void Initialise(Transform player)
    {
        _player = player;
    }

    public void StartMove()
    {
        print("START MOVE");
        _agent.enabled = true;
        _agent.SetDestination(_player.position);
    }

    private void OnDestroy()
    {
        _agent.enabled = false;
    }
}
