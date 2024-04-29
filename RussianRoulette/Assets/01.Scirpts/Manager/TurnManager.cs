using ETC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurnManager : MonoSingleton<TurnManager>
{
    private ChamberState[] _deathBullets = null;
    public ChamberState[] DeathBullets => _deathBullets;

    public int _currentTurn = 0;
    public int CurrentTurn => _currentTurn;

    public bool MyTurn { get; private set; }
    public bool OtherTurn { get; private set; }

    public Action OnFireEvent = null;

    public GunController GunControllCompo { get; set; }

    public override void Awake()
    {
        GunControllCompo = FindObjectOfType<GunController>();
    }

    public void BulletSetting(ChamberState[] chambers)
    {
        _deathBullets = chambers;
    }

    public void ChangeTurn()
    {
        MyTurn = !MyTurn;
        OtherTurn = !OtherTurn;
    }

    public void AddCurrnetStack(int count = 1)
    {
        _currentTurn += count;
    }

    public void GameReset()
    {
        _currentTurn = 0;

        MyTurn = false;
        OtherTurn = false;
    }
}