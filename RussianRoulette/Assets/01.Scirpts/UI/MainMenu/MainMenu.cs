using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private float _fadeTime;

    private TextMeshProUGUI _title;
    private CanvasGroup[] _buttons;

    private void Awake()
    {
        _title = transform.Find("Title").GetComponent<TextMeshProUGUI>();
        _buttons = transform.Find("Buttons").GetComponentsInChildren<CanvasGroup>();

        _title.alpha = 0;
        foreach (var button in _buttons)
        {
            button.alpha = 0;
            button.blocksRaycasts = false;
        }    
    }

    private void Start()
    {
        EnterMenu();
    }

    public void EnterMenu()
    {
        DOTween.KillAll();

        Sequence seq = DOTween.Sequence();

        seq.PrependInterval(1f)
            .Append(_title.DOFade(1, _fadeTime))
            .AppendInterval(1f)
            .AppendCallback(() =>
            {
                 foreach(var button in _buttons)
                 {
                     button.DOFade(1, _fadeTime);
                     button.blocksRaycasts = true;
                 }
            });

        DOTween.Kill(seq);
    }
}
