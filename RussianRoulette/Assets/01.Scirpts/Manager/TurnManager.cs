using ETC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoSingleton<TurnManager>
{
    private bool[] _deathBullets = { false };
    public bool[] DeathBullets => _deathBullets;

    private int _currentTurn = 0;
    public int CurrentTurn => _currentTurn;

    public void StartGameGunSetting(int maxBullet)
    {
        SetDeathBullets(Function.ResetArray<bool>(maxBullet));

        RandomBullet(maxBullet);
    }
    
    public void SetDeathBullets(bool[] deathBullets)
    {
        _deathBullets = deathBullets;
    }

    public void RandomBullet(int maxSize)
    {
        int random = Random.Range(0, maxSize);
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