using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundEnder : MonoBehaviour
{
    [SerializeField] private Finish _finish;
    [SerializeField] private EndPanel _panel;
    [SerializeField] private Player _player;
    [SerializeField] private ObjectDisabler _disabler;

    private void Awake()
    {
        _finish.OnFinish += () => EndRound(true);
        _player.OnDied += () => EndRound(false);
    }

    private void EndRound(bool win)
    {
        _disabler.Disable();
        _panel.Show(win);
        Time.timeScale = 0;
    }
}
