using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip snugglesVoice;
    public AudioClip picklesVoice;
    public AudioClip stevesVoice;
    public AudioClip hacklesVoice;

    public void PlayAudioSnuggles()
    {
        audioSource.clip = snugglesVoice;
        audioSource.Play();
    }

    public void PlayAudioPickles()
    {
        audioSource.clip = picklesVoice;
        audioSource.Play();
    }

    public void PlayAudioSteve()
    {
        audioSource.clip = stevesVoice;
        audioSource.Play();
    }
    public void PlayAudioHackles()
    {
        audioSource.clip = hacklesVoice;
        audioSource.Play();
    }
    public void StopAudioClip()
    {
        audioSource.Stop();
    }
}
