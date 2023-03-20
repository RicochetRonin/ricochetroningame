using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEditor;
using Unity.VisualScripting;


//This script check if trigger enters and exits are from enemies. On enter, activates enemy. On exit, deactivates enemy.
public class PlayerActivateEnemy : MonoBehaviour
{
    public List<GameObject> enemiesOnScreen = new List<GameObject>();
    public EnemyShoot singleEnemy;
    private CinemachineBrain cinemachineBrain;
    public bool enemyActivateBasedOnCamera = true;

    public float shooterFireRateMultiplier = 2.0f;
    public float sniperFireRateMultiplier = 2.0f;
    public float shotgunFireRateMultiplier = 1.5f;
    public float pulseFireRateMultiplier = 1.0f;

    public bool oneEnemyOnScreen;
    public bool fireRateIncreased;
    public bool fireRateDecreased;
    public int numEnemiesOnScreen;

    private void Start()
    {
        cinemachineBrain = Object.FindObjectOfType<CinemachineBrain>();

        if (cinemachineBrain == null  || !enemyActivateBasedOnCamera)
        {
            enemyActivateBasedOnCamera = false;
        }

    }

    private void FixedUpdate()
    {
        numEnemiesOnScreen = enemiesOnScreen.Count;

        if (enemyActivateBasedOnCamera)
        {
            this.gameObject.transform.position = cinemachineBrain.transform.position;
        }


        if (numEnemiesOnScreen == 1 && !fireRateIncreased)
        {
            //Debug.Log("Increase firerate!");
            oneEnemyOnScreen = true;
            fireRateIncreased = true;

            singleEnemy = enemiesOnScreen[0].GetComponentInChildren<EnemyShoot>();

            if (singleEnemy != null)
            {
                if (singleEnemy is SniperShoot)
                {
                    singleEnemy.setMultipliedFireRate(sniperFireRateMultiplier);
                }

                else if (singleEnemy is ShotgunShoot)
                {
                    singleEnemy.setMultipliedFireRate(shotgunFireRateMultiplier);
                }

                else if (singleEnemy is PulseShoot)
                {
                    singleEnemy.setMultipliedFireRate(pulseFireRateMultiplier);
                }

                else if (singleEnemy)
                {
                    singleEnemy.setMultipliedFireRate(shooterFireRateMultiplier);
                }
            }
        }

        else if ((numEnemiesOnScreen > 1 || numEnemiesOnScreen == 0) && fireRateIncreased)
        {
            //Debug.Log("Decrease firerate");
            oneEnemyOnScreen = false;
            fireRateIncreased = false;

            if (singleEnemy != null)
            {
                singleEnemy.setDefaultFireRate();
                singleEnemy = null;
            }
            
        }


    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy") || coll.CompareTag("Boss"))
        {
            var transform = coll.GetComponent<Transform>();
            for (int a = 0; a < transform.childCount; a++)
            {
                transform.GetChild(a).gameObject.SetActive(true);
            }

            if (coll.CompareTag("Enemy") && coll.gameObject.GetComponentInChildren<EnemyShoot>() != null)
            {
                enemiesOnScreen.Add(coll.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy"))
        {
            var transform = coll.GetComponent<Transform>();
            for (int a = 0; a < transform.childCount; a++)
            {
                transform.GetChild(a).gameObject.SetActive(false);
            }
            enemiesOnScreen.Remove(coll.gameObject);
           
        }
    }
}
