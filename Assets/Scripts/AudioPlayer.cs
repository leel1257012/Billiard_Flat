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
    public AudioClip UIMusic;
    public AudioClip ButtonClickMusic;
    public AudioClip LaserMusic;
    public AudioClip HoleMusic;
    public AudioClip WhiteHoleMusic;
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
        Audio.volume = 1;
        Audio.clip = GoalMusic;
        Audio.Play();
    }

    public void PlayCollisionMusic()
    {
        Audio.volume = 1;
        Audio.clip = CollisionMusic;
        Audio.Play();
    }

    public void PlayLaunchMusic()
    {
        Audio.volume = 1;
        Audio.clip = LaunchMusic;
        Audio.Play();
    }

    public void PlayJumpPlatformMusic()
    {
        Audio.volume = 1;
        Audio.clip = JumpPlatformMusic;
        Audio.Play();
    }

    public void PlayGaugebarMusic(bool i)
    {
        Audio.volume = 1;
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
        Audio.volume = 1;
        Audio.clip = PlayerJumpMusic;
        Audio.Play();
    }

    public void PlayUIMusic()
    {
        Audio.volume = 0.25f;
        Audio.clip = UIMusic;
        Audio.Play();
    }

    public void PlayButtonClickMusic()
    {
        Audio.volume = 1;
        Audio.clip = ButtonClickMusic;
        Audio.Play();
    }

    public void PlayHoleMusic()
    {
        Audio.volume = 1;
        Audio.clip = HoleMusic;
        Audio.Play();
    }

    public void PlayWhiteHoleMusic()
    {
        Audio.volume = 1;
        Audio.clip = WhiteHoleMusic;
        Audio.Play();
    }

    public void PlayLaserMusic()
    {
        Audio.volume = 1;
        Audio.clip = LaserMusic;
        Audio.Play();
    }
}
