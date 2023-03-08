using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    [Header("Private Components")]
    private bool canOmniReflect;
    private bool omniReflectActive;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject _aim;
    [SerializeField] private GameObject _aimGraphics;
    [SerializeField] private GameObject _omniReflectHitBox;
    [SerializeField] private GameObject _omniReflectGraphics;
    [SerializeField] private CircleCollider2D _omniReflectCollider;
    //[SerializeField] private ParticleSystem _omniReflectParticleSystem;
    [SerializeField] private Animator _omniReflectAnimator;
    [SerializeField] private RoninSoundManager soundManager;

    //[SerializeField] private AudioClip OmniReflectSFX;

    [Header("Stats")]
    [SerializeField] private float omniReflectDuration = 5f;
    [SerializeField] private float omniReflectCooldown = 30f;

    private PlayerControls _playerControls;

    //public OmniCooldown omniCooldownText; //Attach UI/OmniCooldown to this slot


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
       
        _playerControls = new PlayerControls();
        _omniReflectCollider.enabled = false;
        //_omniReflectGraphics.SetActive(false);
        _omniReflectAnimator.SetFloat("OmniReflectDuration", omniReflectDuration);

        _playerControls.Abilities.OmniReflect.performed += _ => StartCoroutine(OmniReflect());

        omniReflectActive = false;
        canOmniReflect = true;
    }

    #endregion

    private void Update()
    {
        //Updating Omni Reflect UI
        //omniCooldownText.SetCooldown(canOmniReflect);
        if (omniReflectActive)
        {
            return;
        }
    }

    private IEnumerator OmniReflect()
    {
        if (canOmniReflect)
        {
            canOmniReflect = false;
            omniReflectActive = true;
            player.GetComponentInChildren<PlayerHealth>().canTakeDamage = false;
            _omniReflectCollider.enabled = true;
            _omniReflectAnimator.SetTrigger("OmniReflect");
            //_omniReflectGraphics.SetActive(true);

            //AudioManager.PlayOneShotSFX(OmniReflectSFX);
            soundManager.OmniReflect();
            _aim.SetActive(false);
            yield return new WaitForSeconds(omniReflectDuration);
            _omniReflectAnimator.SetTrigger("OmniReflectOver");
            _omniReflectCollider.enabled = false;
            //_omniReflectGraphics.SetActive(false);
            _aim.SetActive(true);
            omniReflectActive = false;
            player.GetComponentInChildren<PlayerHealth>().canTakeDamage = true;
            yield return new WaitForSeconds(omniReflectCooldown);
            canOmniReflect = true;
        }
    }
}
