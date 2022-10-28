using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PulseShoot : EnemyShoot
{

    [SerializeField] protected int numProjectiles = 6;
    [SerializeField] protected int numBursts = 3;
    [SerializeField] protected float burstRate = 1f;



    void Update()
    {
        if (canAttack)
        {
            //Instantiate(bulletPrefab, transform.position, transform.rotation);
            StartCoroutine("PulseSpawn");
            canAttack = false;
            StartCoroutine("ResetCoolDown");
        }
    }

   private void PulseSpawn()
    {
        Debug.Log("Pulse called");
        float angleStep = 360 / numProjectiles;
        float angle = 0;
        float rotation;
        Vector2 currEuler;
        Vector2 newEuler;
        Quaternion newTransformRotation;
        Quaternion currTransformRotation = transform.rotation;
        Quaternion newParentTransformRotation;
        Quaternion currParentTransformRotation = transform.parent.transform.rotation;

        for (int j = 0; j < numBursts; j++)
        {
            for (int i = 0; i < numProjectiles; i++)
            {
                Debug.Log("Shooting " + i);
                Instantiate(bulletPrefab, transform.parent.position, currTransformRotation);
                newTransformRotation = Quaternion.Euler(currTransformRotation.eulerAngles + new Vector3(0, 0, angleStep));
                currTransformRotation = newTransformRotation;
            }
            new WaitForSeconds(burstRate);
        }
        
    }
}
