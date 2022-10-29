using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PulseShoot : EnemyShoot
{

    [SerializeField] protected int numProjectiles = 6;
    [SerializeField] protected int numPulse = 3;
    [SerializeField] protected float timeBetweenPulse = 2f;
    [SerializeField] protected float pulseRadius = 1f;




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
        //Debug.Log("Pulse called");
        float angleStep = 360 / numProjectiles;
        float currAngle = 0;
        Quaternion newTransformRotation;
        Quaternion currTransformRotation = transform.parent.transform.rotation;

        for (int j = 0; j <= numPulse; j++)
        {
            for (int i = 0; i < numProjectiles; i++)
            {
                //Debug.Log("Shooting " + i);
                var offSetx = pulseRadius * Mathf.Cos(currAngle * Mathf.Deg2Rad);
                var offSety = pulseRadius * Mathf.Sin(currAngle * Mathf.Deg2Rad);
                var spawnPosition = transform.parent.transform.parent.position;
                spawnPosition.x += offSetx;
                spawnPosition.y += offSety;
                Instantiate(bulletPrefab, spawnPosition, currTransformRotation);
                newTransformRotation = Quaternion.Euler(currTransformRotation.eulerAngles + new Vector3(0, 0, angleStep));
                currTransformRotation = newTransformRotation;
                currAngle += angleStep;
            }
            new WaitForSeconds(timeBetweenPulse);
        }


        
    }
}
