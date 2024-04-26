using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationAgent : MonoBehaviour
{
    protected Animator anim;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public abstract void StartAnimationTrigger();
    public abstract void EndAnimationTrigger();
}
