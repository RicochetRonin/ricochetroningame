using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerReflect : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The sprite to change colors when detecting")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private RoninSoundManager soundManager;
    
    [SerializeField] private Animator _reflectAnimator;
    [SerializeField] private GameObject _hitParticleSytem;

    private Collider2D _collider;

    private PlayerControls _playerControls;

    [Header("Settings")] [SerializeField] private float detectCoolDown = 0.1f;
    [SerializeField] private Color _cursorColor;
    [SerializeField] private Color _cursorColorDeplete;
    [SerializeField] private LayerMask groundLayer, aimLayer;
    //[SerializeField] private AudioClip SlashSFX;
    //[SerializeField] private AudioClip SlashBulletSFX;

    public bool canReflect;
    private float reflectTime;
    private bool bulletReflected = false;

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
        _spriteRenderer.color = _cursorColor;
        canReflect = true;
        

        _playerControls = new PlayerControls();
        
        _playerControls.Attacking.Reflect.performed += _ => StartCoroutine(Detect());
        foreach (AnimationClip clip in _reflectAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "Reflect")
            {
                reflectTime = clip.length;
            }
        }

    }

    #endregion
    

    //Called when the player attempts to reflect.
    private IEnumerator Detect()
    {
        if (!canReflect)
        {
            yield return null;
        }


        else
        {
            _collider.enabled = true;
            if (!bulletReflected)
            {
                //AudioManager.PlayOneShotSFX(SlashSFX);
                soundManager.Slash();
            }
            canReflect = false;
            _reflectAnimator.SetTrigger("Reflect");
            yield return new WaitForSeconds(reflectTime);
            StartCoroutine("WaitForCoolDown");
        }
    }

    IEnumerator WaitForCoolDown()
    {
        _collider.enabled = false;
        _spriteRenderer.color = _cursorColorDeplete;
        yield return new WaitForSeconds(detectCoolDown);
        _spriteRenderer.color = _cursorColor;
        canReflect = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            //TODO: Change from being a GetComponent to switching the case of the bullet
            other.GetComponent<SpriteRenderer>().color = Color.green;
            _spriteRenderer.sortingOrder = 0;
            other.tag = "PlayerBullet";
            bulletReflected = true;
            //AudioManager.PlayOneShotSFX(SlashBulletSFX);
            soundManager.Reflect();
            bulletReflected = false;
            SleepManager.Sleep(5);
            CinemachineShake.Shake(0.15f, 3f);

        }

        if (other.CompareTag("PlayerBullet"))
        {
            GameManager.bulletsReflected++;
            //TODO: Change from being a GetComponent to switching the case of the bullet
            GameObject particle = Instantiate(_hitParticleSytem, other.transform.position, other.transform.rotation);
            particle.GetComponent<ParticleSystem>().Play();
            bulletReflected = true;
            //AudioManager.PlayOneShotSFX(SlashBulletSFX);
            soundManager.Reflect();
            bulletReflected = false;
            SleepManager.Sleep(2);
            CinemachineShake.Shake(0.1f, 2f);
        }

        //Reflect hit an interactable object
        if (other.CompareTag("Interactable"))
        {
            if (other.GetComponent<UpdateTiles>().isEnabled)
            {
                //Add tiles to the midground
                other.GetComponent<UpdateTiles>().AddMidgroundTile();
            }
            else
            {
                //Removes tiles from midground
                other.GetComponent<UpdateTiles>().RemoveMidgroundTile();
            }
        }
    }
}
