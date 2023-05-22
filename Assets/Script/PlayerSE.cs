using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSE : MonoBehaviour
{
    AudioSource audio_source;
    // Start is called before the first frame update
    void Start()
    {
        audio_source=transform.parent.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void PlaySE(AudioClip se)
    {
        PlaySound.PlaySoundSingle(audio_source,se);
    }
}
