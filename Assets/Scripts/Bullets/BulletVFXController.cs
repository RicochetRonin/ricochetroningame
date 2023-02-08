using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class BulletVFXController : MonoBehaviour
{

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PoolSpawn(Vector3 pos, Quaternion rot, string animation)
    {
        this.transform.parent.gameObject.transform.position = pos;
        this.transform.parent.gameObject.transform.rotation = rot;
        this.transform.parent.gameObject.SetActive(true);
        PlayAnimation(animation);

    }

    public void PlayAnimation(string animation)
    {
        animator.SetTrigger(animation);
    }

    public void AnimationEnd()
    {
        MasterPool.DespawnBulletVFX(this.transform.parent.gameObject);
    }
}
