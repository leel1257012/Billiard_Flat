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
    // Start is called before the first frame update
    void Start()
    {
        Audio = gameObject.AddComponent<AudioSource>();
        Audio.playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
