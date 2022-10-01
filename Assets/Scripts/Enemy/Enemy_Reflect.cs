using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Reflect : MonoBehaviour
{
    [Header("Private Components")]
    private EdgeCollider2D aimCol;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer bulletSprite;

    [Header("Stats")]
    public float reflectCoolDown = 2f;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        aimCol = GetComponent<EdgeCollider2D>();

    }

    //Always collides with bullets because collider is on when not reflecting

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            //Change bullet to friendly
            collision.GetComponent<SpriteRenderer>().color =  Color.red;
            collision.tag = "EnemyBullet";

            aimCol.enabled = false;
            spriteRenderer.color = new Color(255f, 0f, 0f, 0.25f);
            StartCoroutine("ResetReflect");

        }
    }

    private IEnumerator ResetReflect()
    {
        yield return new WaitForSeconds(reflectCoolDown);
        spriteRenderer.color = new Color(255f, 0f, 0f, 0.5f);
        aimCol.enabled = true;
    }
}
