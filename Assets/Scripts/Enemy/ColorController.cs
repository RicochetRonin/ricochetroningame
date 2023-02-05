using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorController : MonoBehaviour
{

    protected SpriteRenderer spriteRenderer;

    /*
    //Incase color should be set from this script
    public Color fadeStartColor;
    public Color fadeEndColor;
    public float fadeDuration = 10;
    */

    public void Start(){
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    public IEnumerator FadeColor(Color startColor, Color endColor, float duration){
        float timeElapsed = 0;
        while (timeElapsed < duration){
            spriteRenderer.color = Color.Lerp(startColor, endColor, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        spriteRenderer.color = endColor;  
    }

}

