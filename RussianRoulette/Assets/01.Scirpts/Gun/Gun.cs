using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public bool Shoot(bool canShoot)
    {
        if (!canShoot)
        {
            return false;
        }

        return Fire();
    }

    private bool Fire()
    {
        return true;
    }
}