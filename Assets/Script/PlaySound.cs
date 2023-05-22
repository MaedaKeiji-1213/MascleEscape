using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaySound 
{
    public static void PlaySoundSingle(AudioSource audio_source,AudioClip audio_clip)
    {
        audio_source.PlayOneShot(audio_clip);
    }
    public static void PlaySoundLoop(AudioSource audio_source,AudioClip audio_clip)
    {
        if(!audio_source.isPlaying||audio_source.clip!=audio_clip)
        {
            audio_source.PlayOneShot(audio_clip);
        }
    }

}
