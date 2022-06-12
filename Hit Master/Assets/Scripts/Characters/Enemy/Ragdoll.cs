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
       //Invoke(nameof(Fall), 2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PunchRigidbody();
            //Fall();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            WriteBones();
        }
    }

    public void Initialise()
    {
        if (_isInitialised)
        {
            _isStangingAfterFalling = _isStandingSave;
        }
        else
        {
            _isStandingSave = _isStangingAfterFalling;
            _isInitialised = true;
        }
    }

    public void Fall()
    {
        WriteBones();
        if (!_isFallen)
        {
            OnFall?.Invoke();

            _isFallen = true;
            SetRigidbodyState(false);
            SetColliderState(true);

            if (_isStangingAfterFalling)
                Invoke(nameof(Stand), _delayToStand);
        }
    }

    public void WriteBones()
    {
        _savedBones = new List<Bone>();
        foreach (var item in _rigidbodies)
        {
            //if (!item.name.Equals(gameObject.name))
                //_ragdollSaver.WriteValue(new Bone(item.name, item.transform.localPosition, item.transform.localRotation));
                _savedBones.Add(new Bone(item.name, item.transform.localPosition, item.transform.localRotation));
        }
    }

    private void Stand()
    {
        if (_isStangingAfterFalling)
        OnBeginStanding?.Invoke();
    }

    public void PunchRigidbody()
    {
        print("PUNCH");
        Fall();
         _rigidbodyToPunch.AddForce(-transform.forward * _forceToPunch, ForceMode.Impulse);
    }


    public void PunchRigidbody(Vector3 position)
    {
        print("PUNCH");
        Fall();
        //_rigidbodyToPunch.AddForce(-transform.forward * _forceToPunch, ForceMode.Impulse);
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
        //PunchRigidbody();
        //Fall();
    }

    private void SetRigidbodyState(bool state)
    {
        if (!_isFoundRigids)
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();//.ToList().Where(r => r.name != gameObject.name).ToArray();
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
       // _playerAnimator.enabled = true;
        _isFallen = false;
    }

    private void BeginStanding()
    {
       // Vector3 headPos = _ragdollSaver.Bones.Where(b => b.Name == _head.name).FirstOrDefault().Position;
     
        StartCoroutine(nameof(LerpHead));
       
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
        RestoreBones();
    }

    private IEnumerator Lerp()
    {
        _temp = new List<Vector3>();
        _temp2 = new List<Quaternion>();
        //store currentPos

        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _temp.Add(_rigidbodies[i].transform.localPosition);
            //print($"{_rigidbodies[i].name} ------ {_savedBones[i].Name}");
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
        OnStandedUp?.Invoke();
    }

    private void SetKinematic()
    {
        GetComponent<Rigidbody>().isKinematic = true;
    }
}
