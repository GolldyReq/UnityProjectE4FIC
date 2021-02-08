using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager m_Instance;
    public static AudioManager Instance { get { return m_Instance; } }

    bool m_IsReady;
    public bool IsReady { get { return m_IsReady; } }

    private void Awake()
    {

        m_IsReady = false;
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
    }

    public void Play(string pathsound)
    {
        Play(pathsound, 1f);
    }

    public void Play(string pathsound , float volume )
    {
        AudioClip clip = Resources.Load(pathsound) as AudioClip;
        this.GetComponent<AudioSource>().PlayOneShot(clip , volume) ;
    }

}
