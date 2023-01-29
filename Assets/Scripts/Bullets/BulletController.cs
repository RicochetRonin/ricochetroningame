using System;
using System.Collections;
using UnityEngine;
//using static UnityEditor.PlayerSettings;

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

    [HideInInspector] public bool playerBullet;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    [Header("Settings")]
    [SerializeField] private float damage = 1f;
    [SerializeField] private float baseSpeed = 5f;
    private float speed;
    [SerializeField] private Vector3 size;
    [SerializeField] private Vector2 direction;
    [SerializeField] private float maxReflects = 5f;
    [SerializeField] private float reflectForce = 5f;
    [SerializeField] private float maxSpeed = 7.5f;
    [SerializeField] private float muzzleFlashTime = 0.1f;
    [SerializeField] private Color muzzleColor1 = Color.white;
    [SerializeField] private Color muzzleColor2 = Color.black;
    [SerializeField] private float maxReflectLifetime = 10.0f;

    [SerializeField] private AudioClip bounceSFX, hitPlayer, reflectedSFX;

    public GameObject bulletVFX;

    private float _reflectCount;
    private float currentReflectLifetime;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        previousPos = transform.position;
        direction = Vector2.up;

        GameObject bulletVFXref = Instantiate(bulletVFX, transform.position, transform.rotation);
        bulletVFXref.GetComponentInChildren<BulletVFXController>().PlayAnimation("MuzzleFlash");
        _reflectCount = 0;
        currentReflectLifetime = 0.0f;
    }

    private void OnEnable()
    {
        speed = baseSpeed;
    }

    private void FixedUpdate()
    {
        Movement();
        RaycastReflect();
        Death();
    }

    private void Movement()
    {
        transform.Translate(direction * Time.deltaTime * speed);
        currentReflectLifetime += Time.deltaTime;

    }

    public void RaycastReflect()
    {
        //Calculating the current direction to fire the ray in said direction
        Vector3 currentDir = CalcDirection();
        RaycastHit2D hit = Physics2D.Raycast(transform.position, currentDir, Time.deltaTime * speed + .1f, collisionMask);
        Debug.DrawRay(transform.position, currentDir, Color.red);

        if (hit.collider != null)
        {
            if (hit.normal.x != 0)
            {
                Vector3 impactRot = new Vector3(0, 0, (hit.normal.x * 90));
                GameObject bulletVFXref = Instantiate(bulletVFX, transform.position, Quaternion.Euler(impactRot));
                //bulletVFXref.GetComponentInChildren<BulletVFXController>().PlayAnimation("Impact");
            }

            else
            {
                Vector3 impactRot = new Vector3(0, 0, 90 + hit.normal.y * 90);
                GameObject bulletVFXref = Instantiate(bulletVFX, transform.position, Quaternion.Euler(impactRot));
                //bulletVFXref.GetComponentInChildren<BulletVFXController>().PlayAnimation("Impact");
            }


            Vector3 reflectDir = Vector3.Reflect(currentDir, hit.normal).normalized;

            float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg - 90;

            transform.eulerAngles = new Vector3(0, 0, rot);

            Debug.DrawRay(transform.position, reflectDir, Color.blue);

            AudioManager.PlayOneShotSFX(bounceSFX);
            _reflectCount++;
            currentReflectLifetime = 0;
        }
    }

    void IncreaseAfterReflect()
    {
        transform.localScale *= 1.05f;
        //To keep track of the size increases in inspector without having to open the transform
        Vector3 size = transform.localScale;
        damage = Mathf.Floor(damage * 1.1f);
    }

    public void PoolSpawn(Vector3 pos, Quaternion rot)
    {
        SetHostile();
        gameObject.transform.position = pos;
        gameObject.transform.rotation = rot;
        gameObject.SetActive(true);
        GameObject bulletVFXref = Instantiate(bulletVFX, transform.position, transform.rotation);
        bulletVFXref.GetComponentInChildren<BulletVFXController>().PlayAnimation("MuzzleFlash");
    }
    
    void Death()
    {
        if (_reflectCount >= maxReflects || currentReflectLifetime > maxReflectLifetime)
        {
            Debug.Log("Here is comparison, current first " + currentReflectLifetime + " " +  maxReflectLifetime);
            MasterPool.DespawnBullet(gameObject);
            _reflectCount = 0;
            currentReflectLifetime = 0;
        }
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
            RaycastReflect();
        }

    }
    
    public void SetFriendly()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        gameObject.tag = "PlayerBullet";
        _spriteRenderer.color = Color.green;
    }
    
    public void SetHostile()
    {
        
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        gameObject.tag = "EnemyBullet";
        _spriteRenderer.color = Color.red;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        //If bullet hits player and the player can take damage and the bullet is an enemy bullet, hurt the player
        if (collision.gameObject.CompareTag("PlayerHurtBox") && collision.gameObject.GetComponentInChildren<PlayerHealth>().getCanTakeDamage() && gameObject.CompareTag("EnemyBullet"))
        {

            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            AudioManager.PlayOneShotSFX(hitPlayer);
            MasterPool.DespawnBullet(gameObject);

        }

        if (gameObject.CompareTag("PlayerBullet"))
        {
            //If the bullet hits an enemy and the bullet is the player's, hurt the enemy
            if (collision.gameObject.CompareTag("EnemyHurtBox"))
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                //deathEffect.transform.localScale *= (1.05f * _reflectCount);
                //MasterPool.DespawnBullet(gameObject);
            }

            //Player reflects a bullet into a bullet interactable trigger
            if (collision.gameObject.CompareTag("BulletInteractable"))
            {
                Debug.Log("PLayer Interactable hit");
            }
        }

        //If the bullet hits the player's reflect collider, calculate bullet reflect direction based on controller
        if (collision.gameObject.CompareTag(("PlayerHitBox")))
        {
            
            playerAim = collision.gameObject.transform.parent.GetComponent<PlayerAim>();

            if (playerAim.usingController)
            {
                transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(playerAim.newDir.y, playerAim.newDir.x) * Mathf.Rad2Deg - 90);
            }
            else
            {
                transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(playerAim.newDir.y, playerAim.newDir.x) * Mathf.Rad2Deg - 90);
            }

            AudioManager.PlayOneShotSFX(reflectedSFX);
            SetFriendly();

            if (speed < maxSpeed)
            {
                speed *= reflectForce;
            }

            currentReflectLifetime = 0.0f;


        }

        //If the bullet hits the Omni-Reflect collider, reflect it
        if (collision.gameObject.CompareTag(("OmniReflectHitBox")))
        {
            SetFriendly();

            Vector3 reflectDirection = (transform.position - collision.gameObject.transform.position);
            var rot = -Mathf.Atan2(reflectDirection.x, reflectDirection.y) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, rot);

            if (speed < maxSpeed)
            {
                speed *= reflectForce + 1;
            }

            currentReflectLifetime = 0.0f;

        }

        //If the the bullet hits an enemy's reflect, reflect the bullet and set it to hostile
        if (collision.gameObject.CompareTag(("EnemyHitBox")))
        {
            enemyAim = collision.gameObject.transform.parent.GetComponent<EnemyAim>();
            
            SetHostile();
            
            float angle = Mathf.Atan2(enemyAim.aimDirection.y, enemyAim.aimDirection.x) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0f, 0f, angle - 90);
            Debug.DrawRay(transform.position, enemyAim.aimDirection, Color.green);

            currentReflectLifetime = 0.0f;

        }
    }
}