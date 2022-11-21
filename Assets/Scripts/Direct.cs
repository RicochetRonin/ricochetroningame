using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direct : MonoBehaviour
{
    [SerializeField] private float deltaPosX, deltaPosY;
    [SerializeField] private float speed = 5;
    [SerializeField] private bool isPingPong, isReverse;
    private float normalizedValue, x, y ,z;
    private Vector2 startPos, endPosition;
    //private Vector2 rectTransformAnchorPos;

    void Awake()
    {
        //rectTransformAnchorPos = GetComponent<RectTransform>().anchoredPosition;
        //Debug.Log(rectTransformAnchorPos);
    }
    void Start()
    {
        startPos = transform.position;
        
        x = startPos.x;
        y = startPos.y;
        z = 0;
    }
    
    void Update()
    {
        if (isPingPong) //Turns on ping pong
        {
            
            if (isReverse) //Reverses the direction
            {
                
                if (deltaPosX != 0)
                {
                    transform.position = new Vector3(startPos.x - Mathf.PingPong(Time.time * speed, deltaPosX), y, z);
                }
                
                if (deltaPosY != 0)
                {
                    transform.position = new Vector3(x, startPos.y - Mathf.PingPong(Time.time * speed, deltaPosY), z);
                }
                
            }
            else
            {
                
                if (deltaPosX != 0)
                {
                    transform.position = new Vector3(startPos.x + Mathf.PingPong(Time.time * speed, deltaPosX), y, z);
                }
                
                if (deltaPosY != 0)
                {
                    transform.position = new Vector3(x, startPos.y + Mathf.PingPong(Time.time * speed, deltaPosY), z);
                }
                
            }
        }
    }
}
