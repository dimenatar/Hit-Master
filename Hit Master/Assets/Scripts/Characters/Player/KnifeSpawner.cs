using System.Collections;
using UnityEngine;

public class KnifeSpawner : MonoBehaviour
{
    [SerializeField] private InputManager _input;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _ignoredLayer;

    [SerializeField] private float _knifeRotationSpeed = 20;
    [SerializeField] private float _speed = 20f;

    [SerializeField] private GameObject _knife;

    private bool _isFirstKnife = true;
    private Vector3 _worldPosition;

    private void Awake()
    {
        _input.OnTouch += (position) => _worldPosition = position;
    }

    public void ThrowKnife()
    {
        if (_knife)
        {
            var ray = _camera.ScreenPointToRay(_worldPosition);
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
                StartCoroutine(CheckDestroy(_knife, 6));
            }
            Physics.IgnoreCollision(_knife.GetComponent<Collider>(), GetComponent<Collider>());
            _knife.transform.parent = null;
            _knife = null;
        }
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
