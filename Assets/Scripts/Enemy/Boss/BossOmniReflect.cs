using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOmniReflect : MonoBehaviour
{
    public bool canOmniReflect = true;

    [Header("References")]
    [SerializeField] private CircleCollider2D _omniReflectCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject shotSpawn;

    [Header("Stats")]
    [SerializeField] private float omniReflectDuration = 5f;
    [SerializeField] private float omniReflectCooldown = 5f;

    public bool getCanOmni()
    {
        return canOmniReflect;
    }

    public IEnumerator StartBossOmniReflect()
    {
        canOmniReflect = false;
        animator.Play("Boss Omni");
        animator.SetTrigger("Start");
        shotSpawn.GetComponent<BossShoot>().enabled = false;
        _omniReflectCollider.enabled = true;
        yield return new WaitForSeconds(omniReflectCooldown);
        _omniReflectCollider.enabled = false;
        animator.SetTrigger("End");
        yield return new WaitForSeconds(omniReflectDuration);
        canOmniReflect = true;
    }
}
