using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IDestroyable
{
    [SerializeField] private GameObject _shatteredVersion;

    public void Destroy()
    {
        Instantiate(_shatteredVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
