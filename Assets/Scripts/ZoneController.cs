using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ZoneController _leftZone, _rightZone;
    //[SerializeField] private Collider2D _collider2D;
    
    [SerializeField] private GameObject[] _enemiesInZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActivateEnemies();
            
            if (_rightZone != null) _rightZone.ActivateEnemies();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_leftZone != null) _leftZone.DeactivateEnemies();
        }
    }

    public void ActivateEnemies()
    {
        foreach (GameObject enemy in _enemiesInZone)
        {
            enemy.GetComponentInChildren<EnemyAim>().enabled = true;
        }
    }

    public void DeactivateEnemies()
    {
        foreach (GameObject enemy in _enemiesInZone)
        {
            enemy.GetComponentInChildren<EnemyAim>().enabled = false;
        }
    }
}
