using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Knife : MonoBehaviour
{
    private bool _isInitialised;
    private Rigidbody _rigidbody;
    private Vector3 _direction;
    private float _speed = 1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isInitialised)
        {
            _rigidbody.velocity = _direction * _speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isInitialised)
        {
            if (other.GetComponent<PunchPart>() && !other.isTrigger)
            {
                other.GetComponent<PunchPart>().GetHit(transform.position);
                Punch(other);
            }
            else if (!other.isTrigger)
            {
                Punch(other);
            }
        }
    }

    public void Initialise(Vector3 direction, float speed)
    {
        _direction = direction;
        _isInitialised = true;
        _rigidbody.isKinematic = false;
        _speed = speed;
    }

    private void Punch(Collider other)
    {
            _isInitialised = false;
             transform.rotation.SetLookRotation(_rigidbody.velocity);
             print(other.name);
            _rigidbody.isKinematic = true;
            //transform.rotation = Quaternion.Euler(_direction);
            transform.SetParent(other.transform);
            Destroy(this);
    }
}
