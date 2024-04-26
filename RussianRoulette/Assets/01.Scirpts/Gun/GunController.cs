using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GunController : MonoBehaviour
{
    #region Componenet
    public Gun GunCompo { get; private set; }
    public GunAnimationTrigger AnimationCompo { get; set; }

    #endregion

    public Action MissileGunEvent = null;
    public Action FireGunEvent = null;

    private TurnManager _turnManager;

    private void Awake()
    {
        GunCompo = GetComponent<Gun>();
        AnimationCompo = transform.Find("Visual").GetComponent<GunAnimationTrigger>();

        _turnManager = TurnManager.Instance;
        _turnManager.StartGameGunSetting(7, Turn.Player); //юс╫ц©о
    }

    public void ShootPlayer(int playerID)
    {
        if(GunCompo.Shoot(_turnManager.DeathBullets[_turnManager.CurrentTurn]))
        {
            FireGunEvent?.Invoke();
            Fire();
        }
        else
        {
            MissileGunEvent?.Invoke();
        }

        _turnManager.AddCurrnetStack();
    }

    public void Fire()
    {
        _turnManager.GameReset();
    }
}