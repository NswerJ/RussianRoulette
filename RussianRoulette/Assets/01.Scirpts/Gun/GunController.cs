using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunController : MonoBehaviour
{
    private Gun _gun;

    public Action MissileGunEvent = null;
    public Action FireGunEvent = null;

    private int _maxBullet;
    public bool[] _deathBullets = { false };

    private void Awake()
    {
        _gun = GetComponent<Gun>();
    }

    //총 게임시작할 때 세팅
    public void StartGameGunSetting(int maxBullet)
    {
        _maxBullet = maxBullet;

        ResetArray(_maxBullet);

        int random = Random.Range(0, _maxBullet);
        _deathBullets[random] = true;
    }

    private void ResetArray(int size)
    {
        bool[] resizeArray = new bool[size];

        for (int i = 0; i < Mathf.Min(size, _deathBullets.Length); i++)
        {
            resizeArray[i] = false;
        }

        _deathBullets = resizeArray;
    }
}