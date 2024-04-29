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

    private TurnManager _turnManager => TurnManager.Instance;

    private void Awake()
    {
        GunCompo = GetComponent<Gun>(); 
        AnimationCompo = transform.Find("Visual").GetComponent<GunAnimationTrigger>();
    }

    public Turn ShootPlayer(Turn turn)
    {
        if(GunCompo.Shoot(_turnManager.DeathBullets[_turnManager.CurrentTurn] == ChamberState.Bullet))
        {
            FireGunEvent?.Invoke();
            Fire();
        }
        else
        {
            MissileGunEvent?.Invoke();
        }

        _turnManager.AddCurrnetStack();

        StartCoroutine(WaitChangeTurn());

        if (turn == Turn.Player) return Turn.Opponent;
        else return Turn.Player;
    }

    private IEnumerator WaitChangeTurn()
    {
        yield return new WaitForSeconds(1f);

        GunCompo.BackToOrigin();

        yield return new WaitForSeconds(2f);

        UIManager.Instance.ShowPopup("TurnPanel");
    }

    public void Fire()
    {
        _turnManager.GameReset();
    }
}