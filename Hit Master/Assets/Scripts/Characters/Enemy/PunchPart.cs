using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPart : MonoBehaviour
{
    public event Action<Vector3> OnHit;

    public void GetHit(Vector3 position) => OnHit?.Invoke(position);
}
