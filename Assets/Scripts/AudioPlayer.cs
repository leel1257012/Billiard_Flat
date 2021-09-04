using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public AudioSource Audio;
    public AudioClip GoalMusic;
    public AudioClip CollisionMusic;
    public AudioClip LaunchMusic;
    public AudioClip JumpPlatformMusic;
    public AudioClip GaugebarMusic;
    public AudioClip PlayerJumpMusic;
    public float vol = 1f;


    private void Awake()
    {
        Audio = gameObject.AddComponent<AudioSource>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Audio.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        Audio.volume = vol;
    }

    public void updateVolume(float volume)
    {
        vol = volume;
    }

    public void cancelAudio()
    {
        Audio.Stop();
        Audio.loop = false;
    }

    public void PlayGoalMusic()
    {
        Audio.clip = GoalMusic;
        Audio.Play();
    }

    public void PlayCollisionMusic()
    {
        Audio.clip = CollisionMusic;
        Audio.Play();
    }

    public void PlayLaunchMusic()
    {
        Audio.clip = LaunchMusic;
        Audio.Play();
    }

    public void PlayJumpPlatformMusic()
    {
        Audio.clip = JumpPlatformMusic;
        Audio.Play();
    }

    public void PlayGaugebarMusic(bool i)
    {
        if(i) 
        {
            Audio.clip = GaugebarMusic;
            Audio.loop = true;
            Audio.Play();
        }
        else 
        {
            Audio.loop = false;
        }
    }

    public void PlayPlayerJumpMusic()
    {
        Audio.clip = PlayerJumpMusic;
        Audio.Play();
    }
}
