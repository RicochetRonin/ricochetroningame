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
            collision.GetComponent<SpriteRenderer>().color =  _color;
            
            collision.tag = "EnemyBullet";

            _aimCol.enabled = false;
            _spriteRenderer.color = new Color(255f, 0f, 0f, 0.25f);
            _spriteRenderer.sortingOrder = 1;
            StartCoroutine("ResetReflect");

        }
    }

    private IEnumerator ResetReflect()
    {
        yield return new WaitForSeconds(reflectCoolDown);
        _spriteRenderer.color = new Color(255f, 0f, 0f, 0.5f);
        _aimCol.enabled = true;
    }
}
