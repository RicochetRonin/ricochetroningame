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
    [SerializeField] private float maxReflectLifetime = 10.0f;
    [SerializeField] private bool rotatesOnImpact = true;

    [SerializeField] private AudioClip bounceSFX, hitPlayer, reflectedSFX;

    public GameObject bulletVFX;

    private float _reflectCount;
    private float currentReflectLifetime;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        previousPos = transform.position;

        if (rotatesOnImpact)
        {
            direction = Vector2.up;
        }

        else
        {
            var radians = (transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
            var x = Mathf.Cos(radians);
            var y = Mathf.Sin(radians);
            direction = new Vector2(x, y);
        }

        MasterPool.SpawnBulletVFX(bulletVFX, transform.position, transform.rotation, "MuzzleFlash");
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
        if (rotatesOnImpact)
        {
            transform.Translate(direction * Time.deltaTime * speed);
        }

        else
        {
            transform.Translate(direction * Time.deltaTime * speed, Space.World);
        }

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
            //Debug.Log("Hit - current direction " + currentDir);
            if (hit.normal.x != 0)
            {
                Vector3 impactRot = new Vector3(0, 0, (hit.normal.x * 90));
                MasterPool.SpawnBulletVFX(bulletVFX, transform.position, Quaternion.Euler(impactRot), "Impact");
            }

            else
            {
                Vector3 impactRot = new Vector3(0, 0, 90 + hit.normal.y * 90);
                MasterPool.SpawnBulletVFX(bulletVFX, transform.position, Quaternion.Euler(impactRot), "Impact");
            }


            Vector3 reflectDir = Vector3.Reflect(currentDir, hit.normal).normalized;
            //Debug.Log("Reflect Direction " + reflectDir);

            float rot = Mathf.Atan2(reflectDir.y, reflectDir.x) * Mathf.Rad2Deg - 90;

            if (rotatesOnImpact)
            {
                transform.eulerAngles = new Vector3(0, 0, rot);
            }

            else
            {
                direction = new Vector2(reflectDir.x, reflectDir.y);
                
            }
            

            Debug.DrawRay(transform.position, reflectDir, Color.blue);

            AudioManager.PlayOneShotSFX(bounceSFX);
            CinemachineShake.Shake(0.05f, 0.2f);
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
        if (rotatesOnImpact)
        {
            direction = Vector2.up;
        }

        else
        {
            var radians = (rot.eulerAngles.z + 90) * Mathf.Deg2Rad;
            var x = Mathf.Cos(radians);
            var y = Mathf.Sin(radians);
            direction = new Vector2(x, y);
        }
        gameObject.transform.position = pos;
        gameObject.transform.rotation = rot;
        gameObject.SetActive(true);
        MasterPool.SpawnBulletVFX(bulletVFX, transform.position, transform.rotation, "MuzzleFlash");

    }
    
    void Death()
    {
        if (_reflectCount >= maxReflects || currentReflectLifetime > maxReflectLifetime)
        {
            MasterPool.DespawnBullet(gameObject);
            _reflectCount = 0;
            currentReflectLifetime = 0;
        }
    }

    
    private Vector3 CalcDirection()
    {
        Vector3 currentDir;

        if (rotatesOnImpact)
        {
            currentDir = (transform.position - previousPos).normalized;
        }
  
        else
        {
            currentDir = new Vector3(direction.x, direction.y, 0);
        }

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

            if (collision.gameObject.CompareTag("BossHurtBox"))
            {
                collision.gameObject.GetComponent<BossHealth>().TakeDamage(damage);
            }

            //Player reflects a bullet into a bullet interactable trigger
            if (collision.gameObject.CompareTag("BulletInteractable"))
            {
                //Debug.Log("PLayer Interactable hit");
            }
        }

        //If the bullet hits the player's reflect collider, calculate bullet reflect direction based on controller
        if (collision.gameObject.CompareTag(("PlayerHitBox")))
        {
            
            playerAim = collision.gameObject.transform.parent.GetComponent<PlayerAim>();

            if (playerAim.usingController)
            {
                if (rotatesOnImpact)
                {
                    transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(playerAim.newDir.y, playerAim.newDir.x) * Mathf.Rad2Deg - 90);

                }

                else
                {
                    direction = new Vector2(playerAim.newDir.x, playerAim.newDir.y);

                }
            }

            else
            {
                if (rotatesOnImpact)
                {
                    transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(playerAim.newDir.y, playerAim.newDir.x) * Mathf.Rad2Deg - 90);
                }

                else
                {
                    direction = new Vector2(playerAim.newDir.x, playerAim.newDir.y);
                }
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

            if (rotatesOnImpact)
            {
                var rot = -Mathf.Atan2(reflectDirection.x, reflectDirection.y) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, rot);
            }
            
            else
            {
                direction = new Vector2 (reflectDirection.x, reflectDirection.y);
            }

            if (speed < maxSpeed)
            {
                speed *= reflectForce + 1;
            }

            currentReflectLifetime = 0.0f;

            SleepManager.Sleep(1);
            CinemachineShake.Shake(0.05f, 1.5f);
        }

        //If a friendly bullet hits the Boss's Omni-Reflect collider, reflect it back as an Enemy Bullet
        if (collision.gameObject.CompareTag(("BossOmniReflectHitbox")))
        {
            SetHostile();

            Vector3 reflectDirection = (transform.position - collision.gameObject.transform.position);

            if (rotatesOnImpact)
            {
                var rot = -Mathf.Atan2(reflectDirection.x, reflectDirection.y) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0, 0, rot);
            }

            else
            {
                direction = new Vector2(reflectDirection.x, reflectDirection.y);
            }

            if (speed < maxSpeed)
            {
                speed *= reflectForce + 1;
            }

            currentReflectLifetime = 0.0f;

            SleepManager.Sleep(1);
            CinemachineShake.Shake(0.05f, 1.5f);
        }

        //If the the bullet hits an enemy's reflect, reflect the bullet and set it to hostile
        if (collision.gameObject.CompareTag(("EnemyHitBox")))
        {
            enemyAim = collision.gameObject.transform.parent.GetComponent<EnemyAim>();
            
            SetHostile();
            
            if (rotatesOnImpact)
            {
                float angle = Mathf.Atan2(enemyAim.aimDirection.y, enemyAim.aimDirection.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0f, 0f, angle - 90);
            }
            
            else
            {
                direction = new Vector2(enemyAim.aimDirection.x, enemyAim.aimDirection.y);
            }

            Debug.DrawRay(transform.position, enemyAim.aimDirection, Color.green);

            currentReflectLifetime = 0.0f;

        }
    }
}