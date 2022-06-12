using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Platforms _platforms;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationDuration;

    private void Awake()
    {
        _platforms.OnCurrentPlatformChanged += (platform) => _agent.SetDestination(platform.StartPlayerPosition);
    }

    public void RotateTowards(Vector3 point, bool disableAgent = true)
    {
        if (disableAgent)
        _agent.isStopped = true;
        StartCoroutine(Rotate(point));
    }

    private IEnumerator Rotate(Vector3 point)
    {
        float timer = 0;
        while (timer < _rotationDuration)
        {
            var lookRotation = Quaternion.LookRotation(point);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, timer / _rotationDuration);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
