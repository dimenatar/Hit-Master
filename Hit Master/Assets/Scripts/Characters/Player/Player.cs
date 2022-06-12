using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnDied;

    public void GetHit()
    {
        OnDied?.Invoke();
    }
}
