using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject _aim;
    [SerializeField] private GameObject _aimGraphics;
    [SerializeField] private GameObject _omniReflectHitBox;
    [SerializeField] private GameObject _omniReflectGraphics;
    [SerializeField] private CircleCollider2D _omniReflectCollider;
    [SerializeField] private ParticleSystem _omniReflectParticleSystem;

    [Header("Stats")]
    [SerializeField] private float omniReflectDuration = 5f;
    [SerializeField] private int maxOmniRelectCapacity = 5;
    [SerializeField] private int currentOmniReflectCapacity = 1;
    [SerializeField] private bool omniReflectActive;

    private PlayerControls _playerControls;
    

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
    }

    #endregion

    private void Update()
    {
        if (omniReflectActive)
        {
            return;
        }
    
    }

    private IEnumerator OmniReflect()
    {
        Debug.Log("OmniReflect called");
        Debug.Log("Current Omni Reflect pouch " + currentOmniReflectCapacity);
        if (currentOmniReflectCapacity > 0 && !omniReflectActive)
        {
            _omniReflectCollider.enabled = true;
            currentOmniReflectCapacity -= 1;
            omniReflectActive = true;
            _omniReflectGraphics.SetActive(true);
            _aim.SetActive(false);
            yield return new WaitForSeconds(omniReflectDuration);
            _omniReflectCollider.enabled = false;
            Debug.Log("Here is the status, should be false " + _omniReflectCollider.enabled);
            _omniReflectGraphics.SetActive(false);
            _aim.SetActive(true);
            omniReflectActive = false;
        }
    }
}
