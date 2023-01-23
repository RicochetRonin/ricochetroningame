using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmAim : MonoBehaviour
{
    private GameObject target;

    //Bool used to set whether the enemy can freely aim at target, or stay locked at one angle
    [SerializeField] private bool canAim = true;

    public GameObject shotSpawn;
    private Transform shotSpawnTransform;

    [HideInInspector] public Vector3 aimDirection;

    private SpriteRenderer enemySprite;


    private void Start()
    {
        enemySprite = transform.parent.GetComponentInChildren<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player");
        shotSpawnTransform = shotSpawn.transform;

    }

    void Update()
    {

        //If enemy can freely aim, aim at the target
        if (canAim)
        {
            aimDirection = (target.transform.position - transform.position).normalized;;
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, angle);


            Vector3 aimLocalScale = Vector3.one;
                
            //Flip enemy sprite and arm depending on aim angle
            if (angle >= -90 && angle <= 90)
            {
                enemySprite.flipX = true;
                aimLocalScale.y = 1f;
                this.transform.localScale = aimLocalScale;
                shotSpawnTransform.localEulerAngles = new Vector3(0, 0, -90);

            }

            else if ((angle <= -90 || angle >= 90))
            {
                enemySprite.flipX = false;
                aimLocalScale.y = -1f;
                this.transform.localScale = aimLocalScale;
                shotSpawnTransform.localEulerAngles = new Vector3(0, 0, 90);
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
