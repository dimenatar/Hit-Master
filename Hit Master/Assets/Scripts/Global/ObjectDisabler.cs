using System.Collections.Generic;
using UnityEngine;

public class ObjectDisabler : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects;

    public void Disable()
    {
        _objects.ForEach(obj => obj.SetActive(false));
    }
}
