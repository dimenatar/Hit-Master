using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private PunchPart _body;
    [SerializeField] private PunchPart _head;
    [SerializeField] private PunchPart _head2;
    [SerializeField] Ragdoll _ragdoll;
    [SerializeField] private int _startHealth;

    private void Awake()
    {
        _body.OnHit += GetHit;
        _head.OnHit += GetSpecialHit;
        _head2.OnHit += GetSpecialHit;
    }

    public void GetHit(Vector3 position)
    {
        _ragdoll.PunchRigidbody(position);
    }

    public void GetSpecialHit(Vector3 position)
    {
        _ragdoll.PunchRigidbody(position);
    }
}
