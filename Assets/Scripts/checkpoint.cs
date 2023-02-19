using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public delegate void SpawnPointEventHandler();
    public static event SpawnPointEventHandler SpawnSet;
    
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
            GameManager.checkPointActive = true;
            GameManager.lastCheckPointPos = new Vector2(transform.position.x, transform.position.y);
            if (SpawnSet != null) SpawnSet();
            _spriteRenderer.sprite = activeSprite;
            Debug.LogFormat("Last Checkpoint is now set to: {0}", gameObject.name);

        } 
    }
}
