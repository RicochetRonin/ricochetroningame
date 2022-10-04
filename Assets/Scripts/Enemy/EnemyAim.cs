using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    private GameObject target;

    [HideInInspector] public Vector3 aimDirection;
    private void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player");

    }
    void Update()
    {

        aimDirection = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);

    }
}
