using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    private Dictionary<string, PopupUI> _popupDictionary = new();
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

    public void AddPopupUI(PopupUI popupUI, string path)
    {
        popupUIList.Add(popupUI);
        _popupDictionary.Add(path, popupUI);
    }

    public void ShowPopup(string path)
    {
        if(_popupDictionary.TryGetValue(path, out PopupUI popupUI))
        {
            popupUI.ShowPanel();
        }
        else
        {
            Debug.LogError("Wrong Path!");
        }
    }
}