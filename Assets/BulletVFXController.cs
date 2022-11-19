using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletVFXController : MonoBehaviour
{

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string animation)
    {
        Debug.Log("Playing animation " + animation);
        animator.SetTrigger(animation);
    }

    public void AnimationEnd()
    {
        //MasterPool.DespawnBulletVFX(gameObject);
        Debug.Log("Destroying bullet VFX");
        Destroy(this);
    }
}
