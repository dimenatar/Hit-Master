using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private List<SkinnedMeshRenderer> _parts;
    [SerializeField] private Material _deadMaterial;

    private void Awake()
    {
        _enemy.OnDied += ChangeMaterials;
    }

    private void ChangeMaterials()
    {
        for (int i = 0; i < _parts.Count; i++)
        {
            _parts[i].material = _deadMaterial;
        }
    }
}
