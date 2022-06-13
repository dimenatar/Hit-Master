using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject _target;

    public void ShowTarget() => _target.SetActive(true);
    public void HideTarget() => _target.SetActive(false);
}
