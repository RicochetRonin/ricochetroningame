using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAwake : MonoBehaviour
{
    [SerializeField] public GameObject awakeTrigger;
    [SerializeField] public Animator animator;
    [SerializeField] public AudioSource _BGM;
    [SerializeField] public AudioClip bossMusic;

    private bool isAwake = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isAwake == false)
        {
            animator.SetTrigger("Awake");
            isAwake = true;
            _BGM.Stop();
            _BGM.PlayOneShot(bossMusic);
            
        }
    }
}