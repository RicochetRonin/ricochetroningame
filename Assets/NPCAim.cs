using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAim : MonoBehaviour
{
    private GameObject target;
    private SpriteRenderer npcSprite;

    private void Start()
    {
        npcSprite = GetComponent<SpriteRenderer>();
        target = GameObject.FindGameObjectWithTag("Player");

    }

    void Update()
    {

        Vector3 directionVector = this.gameObject.transform.InverseTransformPoint(target.transform.position);

        if (directionVector.x > 0) { npcSprite.flipX = true; }
        else if (directionVector.x < 0) { npcSprite.flipX = false; }
    }
         
        

    
}
