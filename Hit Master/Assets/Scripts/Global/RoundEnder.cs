using System.Collections;
using UnityEngine;

public class RoundEnder : MonoBehaviour
{
    [SerializeField] private float _delayToDisplayLose = 1f;
    [SerializeField] private Finish _finish;
    [SerializeField] private EndPanel _panel;
    [SerializeField] private Player _player;
    [SerializeField] private ObjectDisabler _disabler;

    private void Awake()
    {
        _finish.OnFinish += () => StartCoroutine(EndRound(true));
        _player.OnDied += () => StartCoroutine(EndRound(false));
    }

    private IEnumerator EndRound(bool win)
    {
        _disabler.Disable();
        _panel.Show(win);
        if (!win)
        {
            yield return new WaitForSeconds(_delayToDisplayLose);
        }
        Time.timeScale = 0;
    }
}
