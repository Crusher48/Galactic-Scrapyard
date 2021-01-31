using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioObject : MonoBehaviour
{
    public static GameObject audioObjectReference;
    public static GameObject CreateAudioObject(AudioClip soundEffect, Vector3 position, float volume = 1, float pitch = 1)
    {
        GameObject effectObj = Instantiate(audioObjectReference);
        effectObj.transform.position = position;
        effectObj.GetComponent<AudioObject>().Initialize(soundEffect, pitch, volume);
        return effectObj;
    }
    public static GameObject CreateAudioObject(AudioClip soundEffect, float volume = 1, float pitch = 1) { return CreateAudioObject(soundEffect, Vector3.zero, pitch, volume); }

    public AudioSource audioPlayer;
    float lifetime = 0;
    // Start is called before the first frame update
    void Initialize(AudioClip soundEffect, float volume, float pitch)
    {
        audioPlayer.clip = soundEffect;
        lifetime = soundEffect.length;
        audioPlayer.volume = volume;
        audioPlayer.pitch = pitch;
        audioPlayer.Play();
    }
    // Update is called once per frame
    void Update()
    {
        if (lifetime <= 0)
            Destroy(gameObject);
        lifetime -= Time.deltaTime;
    }
}
