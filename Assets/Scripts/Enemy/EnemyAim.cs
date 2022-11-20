using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private bool canAim = true;

    [HideInInspector] public Vector3 aimDirection;

    private SpriteRenderer enemySprite;

    private void Start()
    {
        enemySprite = transform.parent.GetComponentInChildren<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player");

    }
    void Update()
    {

        if (canAim)
        {
            aimDirection = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);
            if (angle >= -90 && angle <= 90 && enemySprite.flipX == false)
            {
                enemySprite.flipX = true;
            }

            else if ((angle <= -90 || angle >= 90) && enemySprite.flipX == true)
            {
                enemySprite.flipX = false;
            }
        }

    }

    public void setCanAim(bool aim)
    {
        canAim = aim;
    }

    internal bool getCanAim()
    {
        return canAim;
    }
}
