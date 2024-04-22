using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimationTrigger : AnimationAgent
{
    private GunController _gunController;

    private readonly int _fire = Animator.StringToHash("Fire");
    private readonly int _missible = Animator.StringToHash("Missible");

    protected override void Awake()
    {
        base.Awake();

        _gunController = transform.parent.GetComponent<GunController>();    
    }

    private void OnEnable()
    {
        _gunController.FireGunEvent += FireAnim;
        _gunController.MissileGunEvent += MissibleAnim;
    }

    private void OnDisable()
    {
        _gunController.FireGunEvent -= FireAnim;
        _gunController.MissileGunEvent -= MissibleAnim;
    }

    public void FireAnim()
    {
        anim.SetTrigger(_fire);
    }
    public void MissibleAnim()
    {
        anim.SetTrigger(_missible);
    }

    public override void StartAnimationTrigger()
    {

    }

    public override void EndAnimationTrigger()
    {

    }
}