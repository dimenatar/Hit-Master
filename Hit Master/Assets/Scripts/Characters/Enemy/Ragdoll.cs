using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using System;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private float _delayToStand = 3;
    [SerializeField] private float _standUpDuration = 0.5f;
    [SerializeField] private float _delayToKinematic = 1;
    [SerializeField] private bool _isStangingAfterFalling = true;
    [SerializeField] private float _forceToPunch;

    [SerializeField] private Collider[] _mainColliders;
    [SerializeField] private Collider[] _triggers;
    [SerializeField] private Rigidbody _head;

    [SerializeField] private Transform _player;
    [SerializeField] private Rigidbody _rigidbodyToPunch;

    [SerializeField] private Transform _camera;

    private List<Bone> _savedBones;

    private bool _isFallen;
    private bool _isStanding;

    private bool _isInitialised;
    private Rigidbody[] _rigidbodies;
    private bool _isFoundRigids;
    private Vector3 _saveHipPos;

    private bool _isStandingSave;
    private float _timer;
    private List<Vector3> _temp;
    private List<Quaternion> _temp2;

    public bool IsFallen => _isFallen;
    public float StandUpDuration => _standUpDuration;

    public event Action OnFall;
    public event Action OnBeginStanding;
    public event Action OnStandedUp;

    private void Awake()
    {
        OnBeginStanding += BeginStanding;
        OnStandedUp += ReturnValuesToStanded;
    }

    private void Start()
    {
        SetRigidbodyState(true);
        SetColliderState(false);
    }

    public void Fall()
    {
        if (_isStanding)
        {
            StopAllCoroutines();
        }
        else if (!_isFallen)
        {
            WriteBones();
        }
        OnFall?.Invoke();

        _isFallen = true;
        SetRigidbodyState(false);
        SetColliderState(true);

        if (_isStangingAfterFalling)
            Invoke(nameof(Stand), _delayToStand);

    }

    public void WriteBones()
    {
        _savedBones = new List<Bone>();
        foreach (var item in _rigidbodies)
        {
            _savedBones.Add(new Bone(item.name, item.transform.localPosition, item.transform.localRotation));
        }
    }

    private void Stand()
    {
        _isStanding = true;
        if (_isStangingAfterFalling)
        OnBeginStanding?.Invoke();
    }

    public void PunchRigidbody()
    {
        Fall();
         _rigidbodyToPunch.AddForce(-transform.forward * _forceToPunch, ForceMode.Impulse);
    }


    public void PunchRigidbody(Vector3 position)
    {
        Fall();
        _rigidbodyToPunch.AddExplosionForce(_forceToPunch, position, 50);
    }

    public void Restore()
    {
        SetRigidbodyState(true);
        SetColliderState(false);
        _isFallen = false;
        OnStandedUp?.Invoke();
    }

    public void FullyFall()
    {
        _isStangingAfterFalling = false;
    }

    private void SetRigidbodyState(bool state)
    {
        if (!_isFoundRigids)
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
            _isFoundRigids = true;
        }
        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
        if (!state)
        {
            Invoke(nameof(SetKinematic), _delayToKinematic);
        }
        else
        GetComponent<Rigidbody>().isKinematic = false;
    }

    private void SetColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        foreach (Collider collider in _mainColliders)
        {
            collider.enabled = !state;
        }
    }

    private void ReturnValuesToStanded()
    {
        SetRigidbodyState(true);
        SetColliderState(false);
        _isFallen = false;
    }

    private void BeginStanding()
    {
        if (_isStangingAfterFalling)
            StartCoroutine(nameof(Lerp));
    }

    private void RestoreBones()
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        if (_isStangingAfterFalling)
            StartCoroutine(nameof(Lerp));
    }

    private IEnumerator LerpHead()
    {
        _timer = 0;
        Bone head = _savedBones.Where(b => b.Name == _head.name).FirstOrDefault();
        Vector3 startPoint = _head.transform.localPosition;
        while (_timer < _standUpDuration/2)
        {
            _head.transform.localPosition = Vector3.Lerp(startPoint, new Vector3(_head.transform.localPosition.x, head.Position.y, _head.transform.localPosition.z), _timer / _standUpDuration/2);
            _timer += Time.deltaTime;
            yield return null;
        }
        //RestoreBones();
        if (_isStangingAfterFalling)
            StartCoroutine(nameof(Lerp));
    }

    private IEnumerator Lerp()
    {
        _temp = new List<Vector3>();
        _temp2 = new List<Quaternion>();
        //store currentPos

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _temp.Add(_rigidbodies[i].transform.localPosition);
            _temp2.Add(_rigidbodies[i].transform.localRotation);
        }

        _timer = 0;
        while (_timer < _standUpDuration)
        {
            for (int i = 0; i < _rigidbodies.Length; i ++)
            {
                _rigidbodies[i].transform.localPosition = Vector3.Lerp(_temp[i], _savedBones[i].Position, _timer / _standUpDuration);
                _rigidbodies[i].transform.localRotation = Quaternion.Slerp(_temp2[i], _savedBones[i].Rotation, _timer / _standUpDuration);
            }
            _timer += Time.deltaTime;
            yield return null;
        }
        _isStanding = false;
        OnStandedUp?.Invoke();
    }

    private void SetKinematic()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
