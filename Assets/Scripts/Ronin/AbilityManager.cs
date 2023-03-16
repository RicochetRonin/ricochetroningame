using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    [Header("Private Components")]
    private bool canOmniReflect;
    private bool omniReflectActive;
    private bool omniParamActive;
    private bool omniSoundPlayed;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject _aimGraphics;
    [SerializeField] private GameObject _omniReflectHitBox;
    [SerializeField] private GameObject _omniReflectGraphics;
    [SerializeField] private CircleCollider2D _omniReflectCollider;
    [SerializeField] public PlayerReflect playerReflect;
    [SerializeField] public SpriteRenderer reflectGraphicsRenderer;
    //[SerializeField] private ParticleSystem _omniReflectParticleSystem;
    [SerializeField] private Animator _omniReflectAnimator, _aimAnimator;
    [SerializeField] private RoninSoundManager soundManager;
    [SerializeField] private SpriteRenderer _aimSpriteRenderer;
    [SerializeField] private Collider2D _aimCollider2D;

    //[SerializeField] private AudioClip OmniReflectSFX;

    [Header("Stats")]
    [SerializeField] private float omniReflectDuration = 5f;
    [SerializeField] private float omniReflectCooldown = 30f;

    private PlayerControls _playerControls;
    private OmniCooldown omniCooldownText;


    // public OmniCooldown omniCooldownText; //Attach UI/OmniCooldown to this slot


    #region Initialization

    private void OnEnable()
    {
        _playerControls.Abilities.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Abilities.Disable();
    }

    private void Awake()
    {
        omniCooldownText = GameObject.FindObjectOfType<OmniCooldown>();
        _playerControls = new PlayerControls();
        _omniReflectCollider.enabled = false;
        //_omniReflectGraphics.SetActive(false);
        _omniReflectAnimator.SetFloat("OmniReflectDuration", omniReflectDuration);

        _playerControls.Abilities.OmniReflect.performed += _ => StartCoroutine(OmniReflect(omniReflectCooldown));

        omniReflectActive = false;
        canOmniReflect = true;
        omniParamActive = false;
    }

    #endregion

    private void Update()
    {
        //Updating Omni Reflect UI
        omniCooldownText.SetCooldown(canOmniReflect);
        if (omniParamActive == true && omniReflectActive == false)
        {
            //omniReflectCooldown = 0;
            StartCoroutine(OmniReflect(0f));
            if (omniSoundPlayed == false)
            {
                omniSoundPlayed = true;
            }
        }

        if (omniReflectActive)
        {
            return;
        }
    }

    /*public bool getCanOmni()
    {
        return canOmniReflect;
    }*/

    public void OmniReflectParamToggle()
    {
        if (omniParamActive == false)
        {
            omniParamActive = true;
        }
        else
        {
            omniParamActive = false;
            omniSoundPlayed = false;
        }
    }

    private IEnumerator OmniReflect(float cooldown)
    {
        if (canOmniReflect)
        {
            playerReflect.canReflect = false;
            canOmniReflect = false;
            omniReflectActive = true;
            player.GetComponentInChildren<PlayerHealth>().canTakeDamage = false;
            _omniReflectCollider.enabled = true;
            _omniReflectAnimator.SetTrigger("OmniReflect");

            if (!omniSoundPlayed)
            {
                soundManager.OmniReflect();
            }

            _aimCollider2D.enabled = false;
            _aimSpriteRenderer.enabled = false;
            //_aimAnimator.enabled = false;

            yield return new WaitForSeconds(omniReflectDuration);
            _omniReflectAnimator.SetTrigger("OmniReflectOver");
            _omniReflectCollider.enabled = false;
            playerReflect.OmniResetReflect();

            if (!omniParamActive)
            {
                playerReflect.canReflect = true;
                _aimCollider2D.enabled = true;
                _aimSpriteRenderer.enabled = true;
                _aimAnimator.enabled = true;
            }
            
            /*_aimCollider2D.enabled = true;
            _aimSpriteRenderer.enabled = true;
            _aimAnimator.enabled = true;*/
            
            omniReflectActive = false;
            player.GetComponentInChildren<PlayerHealth>().canTakeDamage = true;
            yield return new WaitForSeconds(cooldown);

            if (!omniParamActive)
            {
                soundManager.Omniready();
                reflectGraphicsRenderer.sprite = null;
            }

            canOmniReflect = true;
            // omniCooldownText.SetCooldown(true);
        }
    }
}
