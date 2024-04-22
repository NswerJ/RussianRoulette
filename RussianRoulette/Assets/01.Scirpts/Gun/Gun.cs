using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gun : MonoBehaviour
{
    [SerializeField]
    private float _duration;

    public bool Shoot(bool canShoot)
    {
        if (!canShoot)
        {
            return false;
        }

        return true;
    }

    private void Update()//юс╫ц©о
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            OtherTurnShootMotion();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            BackToOrigin();
        }
    }

    public void MyTurnShootMotion()
    {
        transform.DORotate(new Vector3(16.753f, -102.45f, 38.758f), _duration);
        transform.DOMove(new Vector3(3.19f, -0.5f, -0.92f), _duration);
    }

    public void OtherTurnShootMotion()
    {
        transform.DORotate(new Vector3(-1.165f, -86.556f, 6.039f), _duration);
        transform.DOMove(new Vector3(-3.75f, 4.51f, 1.88f), _duration);
    }

    public void BackToOrigin()
    {
        transform.DORotate(new Vector3(43.228f, 14.799f, 34.225f), _duration);
        transform.DOMove(new Vector3(0, 1.2f, 0), _duration);
    }
}