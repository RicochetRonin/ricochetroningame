using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class LaserAim : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private EnemyAim enemyAim;
    private GameObject target;
    private Transform shotSpawn;

    public Color laserColor;


    private void OnEnable()
    {
        lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        lineRenderer.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material.color = laserColor;
        enemyAim = GetComponent<EnemyAim>();
        target = GameObject.FindGameObjectWithTag("Player");
        shotSpawn = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, shotSpawn.position);
        lineRenderer.SetPosition(1, target.transform.position);
        //Debug.Log(lineRenderer.material.color);

    }

    public IEnumerator AlphaFade(float duration)
    {
        float timeElapsed = 0;
        while (timeElapsed < duration)
        {
            Color tempColor = laserColor;
            tempColor.a = 0f;
            lineRenderer.material.color = Color.Lerp(tempColor, laserColor, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        lineRenderer.material.color = laserColor;
    }
}
