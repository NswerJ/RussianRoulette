using ETC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoSingleton<TurnManager>
{
    private bool[] _deathBullets = null;
    public bool[] DeathBullets => _deathBullets;

    private int _currentTurn = 0;
    public int CurrentTurn => _currentTurn;

    public bool MyTurn { get; private set; }
    public bool OtherTurn { get; private set; }

    public void StartGameGunSetting(int maxBullet)
    {
        _deathBullets = Function.ResetArray<bool>(maxBullet);

        int random = Random.Range(0, maxBullet);
        _deathBullets[random] = true;
    }

    public void AddCurrnetStack(int count = 1)
    {
        _currentTurn += count;
    }

    public void GameReset()
    {
        for (int i = 0; i < _deathBullets.Length; i++)
        {
            _deathBullets[i] = false;
        }

        _currentTurn = 0;
    }
}