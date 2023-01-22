using System.Collections;
using UnityEngine;

public class EnterRoom : MonoBehaviour
{
    public bool roomHidden = true;
    public GameObject tilemap;

    //Entering a room
    //Hide Exterior tilemap
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (roomHidden == true)
            {
                roomHidden = false;
                StartCoroutine(FadeOut());
            }
        }
    }

    //Leaving a room
    //Enable exterior tilemap
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (roomHidden == false)
            {
                roomHidden = true;
                StartCoroutine(FadeIn());
            }
        }
    }

    IEnumerator FadeOut()
    {
        Color c = tilemap.GetComponent<Renderer>().material.color;
        c.a = 1;
        for (float alpha = 1f; alpha >= 0; alpha -= 0.1f)
        {
            c.a = alpha;
            tilemap.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(.03f);
        }
        tilemap.SetActive(false);
    }

    IEnumerator FadeIn()
    {
        tilemap.SetActive(true);
        Color c = tilemap.GetComponent<Renderer>().material.color;
        for (float alpha = 0f; alpha <= 1; alpha += 0.1f)
        {
            c.a = alpha;
            tilemap.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(.07f);
        }
    }
}
