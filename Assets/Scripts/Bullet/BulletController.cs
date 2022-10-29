using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Attach to bullet prefab
    //Sebastian Lague's Ricochet: https://www.youtube.com/watch?v=u_p5H0wEN8Y&ab_channel=SebastianLague

    private PlayerAim playerAim;
    private EnemyAim enemyAim;
    private Vector2 targetDir;
    private Rigidbody2D rb;
    
    [SerializeField] private ParticleSystem deathEffect;

    private Collider2D[] colliders;
    //variables for calculating current direction
    private Vector3 previousPos;

    //whatever layer the bullets should reflect on
    [SerializeField] private LayerMask collisionMask;

    [HideInInspector] public bool playerBullet = false;
    private bool _blinking = false;
    private SpriteRenderer _spriteRenderer;
    
    [Header("Settings")]
    [SerializeField] private float damage = 1f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Vector3 size;
    [SerializeField] private float reflectForce = 1.15f;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float maxReflects = 5f;
    [SerializeField] private float muzzleFlashTime = 0.1f;
    [SerializeField] private Color muzzleColor1 = Color.white;
    [SerializeField] private Color muzzleColor2 = Color.black;

    private float _reflectCount = 0f;
    private void Awake()
    {
        previousPos = transform.position;
        direction = Vector2.up;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("MuzzleFlash");
    }

    private void FixedUpdate()
    {
        Movement();
        RaycastReflect();
        //Death();
    }

    private void Movement()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    public void RaycastReflect()
    {
        //Calculating the current direction to fire the ray in said direction
        Vector3 currentDir = CalcDirection();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDir, Time.deltaTime * speed + .1f, collisionMask);
        Debug.DrawRay(transform.position, currentDir, Color.red);
        
        if (hit.collider != null)
        {
            Vector3 reflectDir = Vector3.Reflect(currentDir, hit.normal).normalized;
            Debug.DrawRay(transform.position, reflectDir, Color.blue);

            float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg - 90;
            
            transform.eulerAngles = new Vector3(0, 0, rot);

            //speed *= reflectForce;
            //IncreaseAfterReflect();
            _reflectCount++;
        }
    }

    void IncreaseAfterReflect()
    {
        transform.localScale *= 1.05f;
        //To keep track of the size increases in inspector without having to open the transform
        Vector3 size = transform.localScale;
        damage = Mathf.Floor(damage * 1.1f);
    }

    void Death()
    {
        if (_reflectCount >= maxReflects)
        {
            Destroy(gameObject);
            deathEffect.transform.localScale *= (1.05f * _reflectCount);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator MuzzleFlash()
    {
        Color origColor = _spriteRenderer.color;
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(muzzleFlashTime);
        _spriteRenderer.color = Color.black;
        yield return new WaitForSeconds(muzzleFlashTime);
        _spriteRenderer.color = origColor;
    }
    private Vector3 CalcDirection()
    {

        Vector3 currentDir = (transform.position - previousPos).normalized;
        previousPos = transform.position;
        return currentDir;
        
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.layer == collisionMask.value)
        {
            //Debug.Log("Hit");
            RaycastReflect();
        }

    }
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        
        if (collision.gameObject.CompareTag("PlayerHurtBox") && collision.gameObject.GetComponentInChildren<PlayerHealth>().getCanTakeDamage() && gameObject.CompareTag("EnemyBullet"))
        {

            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject);

        }

        if (collision.gameObject.CompareTag("EnemyHurtBox") && gameObject.CompareTag("PlayerBullet"))
        {

            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);

        }

        if (collision.gameObject.CompareTag(("PlayerHitBox")))
        {
            gameObject.tag = "PlayerBullet";
            playerAim = collision.gameObject.transform.parent.GetComponent<PlayerAim>();
            //Debug.Log(playerAim.usingController);

            if (playerAim.usingController)
            {
                transform.eulerAngles = new Vector3(0f, 0f, -Mathf.Atan2(playerAim.newDir.x, playerAim.newDir.y) * Mathf.Rad2Deg - 90);
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(playerAim.newDir.y, playerAim.newDir.x) * Mathf.Rad2Deg - 90);
            }

            _spriteRenderer.color = Color.green;

            /*
            //speed *= reflectForce;
            //IncreaseAfterReflect();
            //Debug.Log("Hit Bullet");
            
            _reflectCount++;
            */
        }

        
        if (collision.gameObject.CompareTag(("OmniReflectHitBox")))
        {
            gameObject.tag = "PlayerBullet";
            _spriteRenderer.color = Color.green;
            //Debug.Log("Omnit reflect position " + collision.gameObject.transform.position);
            //Debug.Log("Bullet " + transform.position);

            Vector3 reflectDirection = (transform.position - collision.gameObject.transform.position);
            var rot = - Mathf.Atan2(reflectDirection.x, reflectDirection.y) * Mathf.Rad2Deg;
            //Debug.Log("Reflect direction" + reflectDirection);
            //Debug.Log(rot);
            transform.eulerAngles = new Vector3(0, 0, rot);

            //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 180);



        }


        if (collision.gameObject.CompareTag(("EnemyHitBox")))
        {
            gameObject.tag = "EnemyBullet";
            enemyAim = collision.gameObject.transform.parent.GetComponent<EnemyAim>();
            
            _spriteRenderer.color = Color.red;
            
            float angle = Mathf.Atan2(enemyAim.aimDirection.y, enemyAim.aimDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0f, 0f, angle - 90);
            Debug.DrawRay(transform.position, enemyAim.aimDirection, Color.green);
            
            //speed *= reflectForce;
            //IncreaseAfterReflect();
            //Debug.Log("Hit Bullet");
            
            _reflectCount++;
            
        }
    }
}

