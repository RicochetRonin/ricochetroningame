using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReflect : MonoBehaviour
{
    [Header("Private Components")]
    private EdgeCollider2D _aimCol;
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _bulletSprite;

    [Header("Stats")]
    [SerializeField] private float reflectCoolDown = 2f;
    [SerializeField] private Color _color;

    [Header("References")]
    [SerializeField] private Animator bodyAnimator;

    void Start()
    {

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _aimCol = GetComponent<EdgeCollider2D>();

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {

            //Change bullet to friendly
            bodyAnimator.SetTrigger("Reflect");
            collision.GetComponent<SpriteRenderer>().color =  _color;
            
            collision.tag = "EnemyBullet";

            _aimCol.enabled = false;
            _spriteRenderer.color = new Color(255f, 0f, 0f, 1.0f);
            _spriteRenderer.sortingOrder = 1;
            StartCoroutine("ResetReflect");

        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {

            //Change bullet to friendly
            bodyAnimator.SetTrigger("Reflect");

        }
    }

    private IEnumerator ResetReflect()
    {
        yield return new WaitForSeconds(reflectCoolDown);
        _spriteRenderer.color = new Color(255f, 255f, 255f, 1.0f);
        _aimCol.enabled = true;
    }
}
