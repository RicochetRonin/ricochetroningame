using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Reflect : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The sprite to change colors when detecting")][SerializeField] private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;

    private PlayerControls _playerControls;

    [Header("Settings")] [SerializeField] private float detectCoolDown;
    [SerializeField] private Color _color;
    
    #region Initialization
    
    private void OnEnable()
    {
        _playerControls.Aiming.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Aiming.Disable();
    }

    private void Awake()
    {
        _collider = GetComponent<EdgeCollider2D>();

        _playerControls = new PlayerControls();
        
        _playerControls.Attacking.Gamepad.performed += _ => Detect();
        _playerControls.Attacking.Mouse.performed += _ => Detect();
    }

    #endregion

    void Start()
    {
        _spriteRenderer.color = _color;
    }
    private void Detect()
    {
        Debug.Log("Detect");
        _collider.enabled = true;
        _color.a = 0.25f;
        StartCoroutine("WaitForCoolDown");
        _collider.enabled = false;
        _color.a = 0.5f;
    }

    IEnumerator WaitForCoolDown()
    {
        yield return new WaitForSeconds(detectCoolDown);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("EnemyBullet")) return;
        //TODO: Change from being a GetComponent to switching the case of the bullet
        other.GetComponent<SpriteRenderer>().color = _color;
        other.tag = "PlayerBullet";
    }
}
