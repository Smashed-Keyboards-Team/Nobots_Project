using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{

    public enum Sound
    {
        PlayerMove,
        PlayerDash,
        PlayerJump,
        PlayerShock,
        WheelDetect,
        WheelSpin,
        TurretDetect,
        TurretShot,
        TurretBullet,
        Electricity,
        ButtonMouseOver,
        ButtonClick,
        GeneratorEnv,
        GeneratorDestroy,
        DoorEnv,
        DoorOpen,
        PausePanelOpen,
        PausePanelClose,
        Countdown,
        SliderTest,
        Bouncer,
        WinSound,
    }

    public enum Music
    {
        SKTLogo,
        MMM,
        Credits,
        L1,
        L2,
        L3,
        
    }
    //v  v  v  v  Codigo para usar audios  v  v  v  v

    //AudioManager.PlaySound(AudioManager.Sound.XXX);

    //AudioManager.PlaySound(AudioManager.Music.XXX);

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(GetAudioClip(sound));
        Debug.Log("Play: " + sound);
    }

    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found");
        return null;
    }
}
 