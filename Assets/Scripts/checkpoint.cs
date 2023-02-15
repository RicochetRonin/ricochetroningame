using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite, inactiveSprite;
    private SpriteRenderer _spriteRenderer;


/*    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }*/


    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = inactiveSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {  
        if (other.gameObject.CompareTag("Player"))
        {
            /*Debug.Log(transform);*/
            GameManager.lastCheckPointPos = transform;
            _spriteRenderer.sprite = activeSprite;
            /*Debug.LogFormat("Last Checkpoint: {0}", gameObject.name);*/
            /*Debug.Log("Checkpoint!");*/
        } 
    }
}
