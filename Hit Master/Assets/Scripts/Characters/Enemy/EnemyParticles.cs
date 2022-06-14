using System.Collections;
using UnityEngine;

public class EnemyParticles : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private ParticleSystem _circle1;
    [SerializeField] private ParticleSystem _circle2;
    [SerializeField] private ParticleSystem _stars;
    [SerializeField] private GameObject _particles;

    [SerializeField] private float _particlesDuration = 1f;
    [SerializeField] private float _delayToShowSecondCircle = 1;

    private void Awake()
    {
        _enemy.OnHit += RelocateParticles;
        _enemy.OnHit += (position) => StartCoroutine(ShowParticles());
    }

    private IEnumerator ShowParticles()
    {
        Play();
        yield return new WaitForSeconds(_delayToShowSecondCircle);
        _circle2.Play();
        yield return new WaitForSeconds(_particlesDuration - _delayToShowSecondCircle);
        StopPlaying();
    }

    private void Play()
    {
        _circle1.Play();
        _stars.gameObject.SetActive(true);
    }

    private void StopPlaying()
    {
        _circle1.Stop();
        _circle2.Stop();
        _stars.gameObject.SetActive(false);
    }

    private void RelocateParticles(Vector3 pos)
    {
        var positon = _particles.transform.position;
        var z = _particles.transform.localPosition.z;
        _particles.transform.position = pos;
        _particles.transform.localPosition = new Vector3(_particles.transform.localPosition.x, _particles.transform.localPosition.y, z);
    }
}
