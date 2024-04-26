using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnUI : PopupUI
{
    private TextMeshProUGUI _turnText;

    public override void Awake()
    {
        base.Awake();

        _turnText = transform.Find("TurnInfoText").GetComponent<TextMeshProUGUI>();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        _turnText.text = TurnTextSetting();
    }

    private string TurnTextSetting()
    {
        string turn = string.Empty;

        if (TurnManager.Instance.MyTurn) turn = "My Turn";
        else turn = "Other Turn";

        return turn;
    }
}
