using System;
using UnityEngine;

public class PunchPart : MonoBehaviour
{
    public event Action<Vector3, GameObject> OnHit;

    public void GetHit(Vector3 position, GameObject knife) => OnHit?.Invoke(position, knife);
}
