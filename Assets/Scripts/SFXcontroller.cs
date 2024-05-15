using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXcontroller : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;
    public AudioClip bomb;

    public void PlayExplosion()
    {
        SFXSource.clip = bomb;
        SFXSource.Play();
    }
}
