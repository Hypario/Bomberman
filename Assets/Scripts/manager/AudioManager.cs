using UnityEngine.Audio;
using System;
using UnityEngine;

// allow us to get acces to the sound from everywhere
public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    public AudioMixerGroup audioMixerGroup;
    public Sound[] sounds;

    private void Awake()
    {
        // singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // foreach sound we want them to have the AudioSource component to display sound
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.outputAudioMixerGroup = audioMixerGroup;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    // launch the main theme sound when starting the game
    private void Start()
    {
        Play("Theme");
    }

    // play the sound when found
    public void Play(string name)
    {
        // search for the sound in the array of Sound
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            // if it is not found, an error is thrown in the inspector (can't be seen when build)
            Debug.LogWarning("Sound : " + name + " not found !");
            return;
        }
        // if found, we play it
        s.source.Play();
    }
}
