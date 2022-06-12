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
    private float _rotationSpeed;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_isInitialised)
        {
            RotateAlongX();
            _rigidbody.velocity = _direction * _speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isInitialised)
        {
            if (other.GetComponent<PunchPart>() && !other.isTrigger)
            {
                Punch(other);
                other.GetComponent<PunchPart>().GetHit(transform.position, gameObject);
            }
            else if (!other.isTrigger)
            {
                var root = IsParentImplementPunchPart(other.transform);
                if (root)
                {
                    root.GetComponent<PunchPart>().GetHit(transform.position, gameObject);
                }
                Punch(other);
            }
        }
    }

    public void Initialise(Vector3 direction, float speed, float rotationSpeed)
    {
        _direction = direction;
        _isInitialised = true;
        _rigidbody.isKinematic = false;
        _speed = speed;
        _rotationSpeed = rotationSpeed;
        //StartCoroutine(Rotate());
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

    private void RotateAlongX()
    {
        transform.Rotate(new Vector3(_rotationSpeed * Time.deltaTime, 0, 0));
    }

    private Transform IsParentImplementPunchPart(Transform node)
    {
        if (node != null)
        {
            if (node.GetComponent<PunchPart>())
            {
                return node;
            }
            else
            {
                return IsParentImplementPunchPart(node.parent);
            }
        }
        else return null;
    }

}
