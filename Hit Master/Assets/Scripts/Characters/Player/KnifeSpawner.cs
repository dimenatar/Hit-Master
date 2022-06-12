using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _ignoredLayer;

    [SerializeField] private float _knifeRotationSpeed = 20;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _delayToSpawn = 0.5f;
    [SerializeField] private float _delayToThrow = 0.5f;
    [SerializeField] private float _delayToBecomeReadyToHit = 0.2f;

    [SerializeField] private GameObject _knife;

    private bool _isFirstKnife = true;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(ThrowKnife(Input.mousePosition));
        }
    }

    public IEnumerator ThrowKnife(Vector3 screenSpace)
    {
        yield return new WaitForSeconds(_delayToThrow);
        if (_knife)
        {
            var ray = _camera.ScreenPointToRay(screenSpace);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _ignoredLayer))
            {
                if (hit.collider != null)
                {
                    _knife.GetComponent<Knife>().Initialise((hit.point - _knife.transform.position).normalized, _speed, _knifeRotationSpeed);
                    _knife.transform.LookAt(hit.point);
                }
            }
            else
            {
                _knife.GetComponent<Knife>().Initialise(ray.direction, _speed, _knifeRotationSpeed);
                _knife.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(_knife.transform.forward, ray.direction, 1, 0));
                print("else");
                Destroy(_knife, 10);
            }
            _knife.transform.parent = null;
            _knife = null;
        }
        yield return new WaitForSeconds(_delayToSpawn);
        SpawnKnife();
    }

    public void SpawnKnife()
    {
        _knife = Instantiate(_prefab, _spawnPoint);
        _knife.transform.localRotation = Quaternion.identity;
    }

    private IEnumerator CheckDestroy(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (obj.transform.parent == null)
        {
            Destroy(obj);
        }
    }
}
