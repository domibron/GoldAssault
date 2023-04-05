using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    public bool isMuted = false;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSource.mute != isMuted)
        {
            audioSource.mute = isMuted;
        }
    }

    public void MuteAndUnmute()
    {
        isMuted = !isMuted;
    }
}
