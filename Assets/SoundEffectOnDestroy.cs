using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectOnDestroy : MonoBehaviour
{
    [SerializeField] AudioClip soundEffect;
    bool createAudio = true;
    private void OnApplicationQuit()
    {
        createAudio = false;
    }
    private void OnDestroy()
    {
        if (soundEffect != null && createAudio)
            AudioObject.CreateAudioObject(soundEffect, transform.position);
    }
}
