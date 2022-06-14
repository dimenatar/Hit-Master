using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private Platforms _platforms;
    [SerializeField] private Animator _animator;
    [SerializeField] private InputManager _input;
    [SerializeField] private NavMeshAgent _agent;

    private readonly int THROW = Animator.StringToHash("Throw");
    private readonly int MOVE = Animator.StringToHash("Move");
    private readonly int STOP_MOVING = Animator.StringToHash("StopMove");

    private void Awake()
    {
        _input.OnTouch += (position) => _animator.SetTrigger(THROW);
        _platforms.OnCurrentPlatformChanged += (platforms) => StartMoving();
    }

    private void StartMoving()
    {
        _animator.SetTrigger(MOVE);
        _platforms.CurrentPlatform.OnPlayerEntersPlatform += StopMoving;
    }

    private void StopMoving()
    {
        _animator.SetTrigger(STOP_MOVING);
    }
}
