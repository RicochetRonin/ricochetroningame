using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoninSoundManager : MonoBehaviour
{
    [SerializeField] private float timeBetweenSteps = 1.5f;

    [Header("AudioSources")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource abilitySource;

    [Header("AudioClips")]
    [SerializeField] AudioClip[] footsteps;
    [SerializeField] AudioClip omniReflectSFX;
    [SerializeField] AudioClip omniReadySFX;
    [SerializeField] AudioClip reflectSFX;
    [SerializeField] AudioClip slashSFX;
    [SerializeField] AudioClip jumpSFX;
    [SerializeField] AudioClip landSFX;
    [SerializeField] AudioClip wallClingSFX;
    [SerializeField] AudioClip damageTakeSFX;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip dashSFX;


    private bool canPlayStep = true;

   public void Footstep()
    {
        if (canPlayStep)
        {
            canPlayStep = false;
            audioSource.PlayOneShot(footsteps[Random.Range(0, footsteps.Length - 1)]);
            StartCoroutine(UntilNextStep());
        }
    }
    public void OmniReflect()
    {
        abilitySource.PlayOneShot(omniReflectSFX);
    }
    public void Omniready()
    {
        abilitySource.PlayOneShot(omniReadySFX);
    }
    public void Reflect()
    {
        abilitySource.PlayOneShot(reflectSFX);
    }
    public void Slash()
    {
        abilitySource.PlayOneShot(slashSFX);
    }
    public void Jump()
    {
        abilitySource.PlayOneShot(jumpSFX);
    }
    public void Land()
    {
        abilitySource.PlayOneShot(landSFX);
    }
    public void WallCling()
    {
        abilitySource.PlayOneShot(reflectSFX);
    }
    public void DamageTake()
    {
        abilitySource.PlayOneShot(damageTakeSFX);
    }
    public void Death()
    {
        abilitySource.PlayOneShot(deathSFX);
    }
    public void Dash()
    {
        abilitySource.PlayOneShot(dashSFX);
    }

    public IEnumerator UntilNextStep()
    {
        yield return new WaitForSeconds(timeBetweenSteps);
        canPlayStep = true;
    }
}
