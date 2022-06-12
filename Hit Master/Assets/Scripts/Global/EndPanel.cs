using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EndPanel : MonoBehaviour
{
    [SerializeField] private Levels _levels;
    [SerializeField] private EnemyCounter _enemyCounter;

    [SerializeField] private GameObject _winButton;
    [SerializeField] private GameObject _loseButton;
    [SerializeField] private GameObject _kills;

    [SerializeField] private Image _panelBackground;
    [SerializeField] private Image _levelBackground;

    [SerializeField] private TextMeshProUGUI _result;
    [SerializeField] private TextMeshProUGUI _killsText;

    [SerializeField] private float _panelFillDuration = 0.5f;
    [SerializeField] private float _panelFillAlpha = 0.3f;

    [SerializeField] private float _delayToShowKills = 0.5f;
    [SerializeField] private float _delayToShowButton = 0.5f;

    [SerializeField] private float _killsAnimationDuration = 0.5f;
    [SerializeField] private float _buttonAnimationDuration = 0.5f;

    #region Text Constants
    private const string WIN_TEXT_1 = "LEVEL";
    private const string WIN_TEXT_2 = "\nCOMPLITED";

    private const string LOSE_TEXT_1 = "LEVEL";
    private const string LOSE_TEXT_2 = "\nFAILED";
    #endregion

    private GameObject _button;

    public void Show(bool win)
    {
        StartCoroutine(ShowPanel(win));
    }

    private IEnumerator ShowPanel(bool win)
    {
        if (win)
        {
            _button = _winButton;
            _result.text = $"{WIN_TEXT_1} {_levels.CurrentLevel}{WIN_TEXT_2}";
        }
        else
        {
            _button = _loseButton;
            _result.text = $"{LOSE_TEXT_1} {_levels.CurrentLevel}{LOSE_TEXT_2}";
        }

        _kills.transform.localScale = Vector3.zero;
        _button.transform.localScale = Vector3.zero;

        _panelBackground.gameObject.SetActive(true);
        _button.SetActive(true);

        _killsText.text = $"x{_enemyCounter.StartAmount - _enemyCounter.Counter}";

        // animate images and text's alpha 
        _panelBackground.DOColor(ChangeAlpha(_panelBackground.color, _panelFillAlpha), _panelFillDuration).SetUpdate(true);
        _levelBackground.DOColor(ChangeAlpha(_levelBackground.color, 1), _panelFillDuration).SetUpdate(true);
        _result.DOColor(ChangeAlpha(_result.color, 1), _panelFillDuration).SetUpdate(true);

        // wait for scaling kills
        yield return new WaitForSeconds(_delayToShowKills);

        // animate scaling kills
        _kills.transform.DOScale(1, _killsAnimationDuration).SetUpdate(true);

        //wait for scaling result button
        yield return new WaitForSeconds(_delayToShowButton);

        //animate button
        _button.transform.DOScale(1, _buttonAnimationDuration).SetUpdate(true);
    }

    private Color ChangeAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }
}
