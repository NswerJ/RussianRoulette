using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public List<PopupUI> popupUIList = new();

    private void Start()
    {
        AllHidePanel();
    }

    public void AllHidePanel()
    {
        foreach (var popup in popupUIList)
        {
            popup.HidePanel();
        }
    }

    public void AddPopupUI(PopupUI popupUI)
    {
        popupUIList.Add(popupUI);
    }

    public void OnPopupUIShow(PopupUI popupUI)
    {

    }
}