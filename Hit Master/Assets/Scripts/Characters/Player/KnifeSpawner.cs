using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _ignoredLayer;

    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _delayToSpawn = 0.5f;
    [SerializeField] private float _delayToThrow = 0.5f;
    [SerializeField] private float _delayToBecomeReadyToHit = 0.2f;

    private GameObject _knife;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnKnife();
            ThrowKnife(Input.mousePosition);
        }
    }

    public void ThrowKnife(Vector3 screenSpace)
    {
        if (_knife)
        {
            var ray = _camera.ScreenPointToRay(screenSpace);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, _ignoredLayer))
            {
                if (hit.collider != null)
                {
                    _knife.GetComponent<Knife>().Initialise((hit.point - _knife.transform.position).normalized, _speed);
                }
            }
            else
            {
                _knife.GetComponent<Knife>().Initialise(ray.direction, _speed);
                Destroy(_knife, 10);
            }
        }
    }

    public void SpawnKnife()
    {
        _knife = Instantiate(_prefab, _spawnPoint);
    }
}
