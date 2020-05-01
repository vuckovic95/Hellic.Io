using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using NaughtyAttributes;

public class AudioManager : MonoBehaviour
{
    [BoxGroup("Audio Sources")] public AudioSource playerSource;
    [BoxGroup("Audio Sources")] public AudioSource environmentSource;
    [BoxGroup("Audio Clips")] public AudioClip win;

    private int muted = 0;

    private void Awake()
    {
        GlobalManager.AudioManager = this;
    }

    void Start()
    {
        muted = PlayerPrefs.GetInt("muted", 0);
        UpdateSources();
    }


    public void UpdateSources()
    {
        if (muted == 0)
        {
            playerSource.mute = false;
            environmentSource.mute = false;
            GlobalManager.UI_Manager.UpdateSoundImage(true);
        }
        else
        {
            playerSource.mute = true;
            environmentSource.mute = true;
            GlobalManager.UI_Manager.UpdateSoundImage(false);
        }
    }

    public void PlayWin()
    {
        if (muted == 0 && playerSource != null & win != null)
        {
            playerSource.clip = win;
            playerSource.Play();
        }
    }


    public void ToggleMute()
    {
        if (muted == 0)
        {
            muted = 1;
            UpdateSources();
        }
        else
        {
            muted = 0;
            UpdateSources();
        }
        PlayerPrefs.SetInt("muted", muted);
    }
}
