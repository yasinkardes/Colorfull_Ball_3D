using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    [Header("Source")]
    public AudioSource buttonSource;
    public AudioSource blowUpSource;
    public AudioSource cashSource;
    public AudioSource completeSource;
    public AudioSource objectHitSource;

    [Header("Clip")]
    public AudioClip buttonClip;
    public AudioClip blowUpClip;
    public AudioClip cashClip;
    public AudioClip completeClip;
    public AudioClip objectHitClip;

    public void ButtonSound()
    {
        buttonSource.PlayOneShot(buttonClip);
    }

    public void BlowUpSound()
    {
        blowUpSource.PlayOneShot(blowUpClip, 0.3f);
    }

    public void CashSound()
    {
        cashSource.PlayOneShot(cashClip);
    }

    public void CompletedSound()
    {
        completeSource.PlayOneShot(completeClip);
    }

    public void ObjectHitSound()
    {
        objectHitSource.PlayOneShot(objectHitClip);
    }
}
