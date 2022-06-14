using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Enemy>())
        {
            other.GetComponent<Enemy>().Die();
        }
    }
}
