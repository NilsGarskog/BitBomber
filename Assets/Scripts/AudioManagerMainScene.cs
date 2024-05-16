using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMainScene : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    public AudioClip ArrowHit;
    public AudioClip SlowedDown;
    public AudioClip SkullHit;
    public AudioClip Shield;
    public AudioClip SuddenDeath;

    public void arrowHit()
    {
        musicSource.PlayOneShot(ArrowHit);
    }
    public void slowedDown()
    {
        musicSource.PlayOneShot(SlowedDown);
    }
    public void skullHit()
    {
        musicSource.PlayOneShot(SkullHit);
    }
    public void shield()
    {
        musicSource.PlayOneShot(Shield);
    }
    public void suddenDeath()
    {
        musicSource.PlayOneShot(SuddenDeath);
    }

}
