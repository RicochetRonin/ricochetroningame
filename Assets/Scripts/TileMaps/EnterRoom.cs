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
                tilemap.SetActive(false);

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
                tilemap.SetActive(true);

            }
        }
    }
}
