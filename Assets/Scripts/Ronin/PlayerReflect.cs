using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReflect : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The sprite to change colors when detecting")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    //[SerializeField] private ParticleSystem _reflectParticleSystem;
    [SerializeField] private Animator _reflectAnimator;
    [SerializeField] private GameObject _hitParticleSytem;

    private Collider2D _collider;

    private PlayerControls _playerControls;

    [Header("Settings")] [SerializeField] private float detectCoolDown;
    [SerializeField] private Color _color;
    [SerializeField] private LayerMask groundLayer, aimLayer;
    [SerializeField] private AudioClip reflectSFX;
        
    public bool canReflect;
    
    #region Initialization
    
    private void OnEnable()
    {
        _playerControls.Attacking.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Attacking.Disable();
    }

    private void Awake()
    {
        _collider = GetComponent<EdgeCollider2D>();
        _spriteRenderer.color = _color;

        _playerControls = new PlayerControls();
        
        _playerControls.Attacking.Reflect.performed += _ => Detect();

        //Ignores colliding with Aim, Ground
        //Physics.IgnoreLayerCollision(aimLayer, groundLayer);
    }

    #endregion
    
    private void Detect()
    {
        if (!canReflect) return;
        
        //Debug.Log("Detect");

        //_reflectParticleSystem.Play();
        //AudioManager.PlayOneShotSFX(reflectSFX);
        
        //Debug.Log("Triggered!");
        _reflectAnimator.SetTrigger("Reflect");

        _collider.enabled = true;
        _color.a = 0.25f;
        StartCoroutine("WaitForCoolDown");
    }

    IEnumerator WaitForCoolDown()
    {
        yield return new WaitForSeconds(detectCoolDown);
        _collider.enabled = false;
        _color.a = 0.5f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("COLLISION " + other.tag);
        if (other.CompareTag("EnemyBullet"))
        {
            //TODO: Change from being a GetComponent to switching the case of the bullet
            other.GetComponent<SpriteRenderer>().color = Color.green;
            _spriteRenderer.sortingOrder = 0;
            other.tag = "PlayerBullet";
        }

        if (other.CompareTag("PlayerBullet"))
        {
            //TODO: Change from being a GetComponent to switching the case of the bullet
            //Debug.Log("SHOULD SEE HIT EFFECT");
            GameObject particle = Instantiate(_hitParticleSytem, other.transform.position, other.transform.rotation);
            particle.GetComponent<ParticleSystem>().Play();
        }
    }
}