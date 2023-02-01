using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    private GameObject target;

    //Bool used to set whether the enemy can freely aim at target, or stay locked at one angle
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
        if (Physics2D.Linecast(transform.position, target.transform.position, 1 << 8))
        {
            setCanAim(false);
        }
        else
        {
            setCanAim(true);
        }

        //If enemy can freely aim, aim at the target
        if (canAim)
        {
            aimDirection = (target.transform.position - transform.position).normalized;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);

            //Flip enemy sprite depending on aim angle
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
