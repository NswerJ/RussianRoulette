using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    [SerializeField]
    protected float duration = 1;

    protected CanvasGroup _canvasGroup;

    public virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        UIManager.Instance.AddPopupUI(this, this.gameObject.name);
    }

    public virtual void HidePanel()
    {
        _canvasGroup.DOFade(0, duration);
        _canvasGroup.blocksRaycasts = false;
    }

    public virtual void ShowPanel()
    {
        _canvasGroup.DOFade(1, duration);
        _canvasGroup.blocksRaycasts = true;
    }
}