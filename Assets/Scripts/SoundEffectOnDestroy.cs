using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundEffectOnDestroy : MonoBehaviour
{
    [SerializeField] AudioClip soundEffect;
    bool createAudio = true;
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        createAudio = false;
    }
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
