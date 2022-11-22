using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    [Header("Private Components")]
    private bool canOmniReflect;
    private bool omniReflectActive;

    [Header("References")]
    [SerializeField] private GameObject _aim;
    [SerializeField] private GameObject _aimGraphics;
    [SerializeField] private GameObject _omniReflectHitBox;
    [SerializeField] private GameObject _omniReflectGraphics;
    [SerializeField] private CircleCollider2D _omniReflectCollider;
    [SerializeField] private ParticleSystem _omniReflectParticleSystem;
    [SerializeField] private AudioClip playSFX;

    [Header("Stats")]
    [SerializeField] private float omniReflectDuration = 5f;
    [SerializeField] private float omniReflectCooldown = 30f;

    private PlayerControls _playerControls;

    public OmniCooldown omniCooldownText; //Attach UI/OmniCooldown to this slot


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
        //_omniReflectCollider = GetComponentInChildren<CircleCollider2D>();
        //_omniReflectParticleSystem = GetComponentInChildren<ParticleSystem>();

        _playerControls = new PlayerControls();
        _omniReflectCollider.enabled = false;
        _omniReflectGraphics.SetActive(false);

        _playerControls.Abilities.OmniReflect.performed += _ => StartCoroutine(OmniReflect());

        omniReflectActive = false;
        canOmniReflect = true;
    }

    #endregion

    private void Update()
    {
        omniCooldownText.SetCooldown(canOmniReflect);
        if (omniReflectActive)
        {
            return;
        }
    
    }

    private IEnumerator OmniReflect()
    {
        Debug.Log("OmniReflect called");
        if (canOmniReflect)
        {
            canOmniReflect = false;
            omniReflectActive = true;
            _omniReflectCollider.enabled = true;
            _omniReflectGraphics.SetActive(true);
            AudioManager.PlayOneShotSFX(playSFX);
            _aim.SetActive(false);
            yield return new WaitForSeconds(omniReflectDuration);
            _omniReflectCollider.enabled = false;
            Debug.Log("Here is the status, should be false " + _omniReflectCollider.enabled);
            _omniReflectGraphics.SetActive(false);
            _aim.SetActive(true);
            omniReflectActive = false;
            yield return new WaitForSeconds(omniReflectCooldown);
            canOmniReflect = true;
        }
    }
}
