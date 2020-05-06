using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    private static int sueldoDJ = 1750;
    private static GameObject DJ;

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
        ML1,
        ML2,
        ML3, 
    }

    //v  v  v  v  Codigo para usar audios  v  v  v  v

    //AudioManager.PlaySound(AudioManager.Sound.XXX);

    //AudioManager.PlaySound(AudioManager.Music.XXX);

    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        soundGameObject.AddComponent<SoundAutoPauser>();    // Auto pauser
        audioSource.PlayOneShot(GetAudioClip(sound));
        Debug.Log("Play: " + sound);
        //Destroy
    }
    public static void PlaySound(Sound sound, Transform emisor)
    {
        GameObject soundGameObject = new GameObject("Sound");
        soundGameObject.transform.position = emisor.position;
        soundGameObject.transform.SetParent(emisor);
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        soundGameObject.AddComponent<SoundAutoPauser>();    // Auto pauser
        audioSource.clip = GetAudioClip(sound);
        audioSource.spatialBlend = 1;
        audioSource.rolloffMode = AudioRolloffMode.Linear;
        audioSource.dopplerLevel = .8f;
        audioSource.Play();
        Debug.Log("Play: " + sound);
        //Destroy
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

    public static void PlayMusic(Music music)
    {
        if (DJ == null) // Mirar si no tenemos DJ
        {
            // Contratar DJ
            DJ = ContratarDJ(sueldoDJ);
        }

        // Decirle que ponga el disco "music" con la DJHand
        AudioSource DJHand = DJ.GetComponent<AudioSource>();
        if (DJHand.clip != GetMusicClip(music))     //Solo cambiar en caso de cambiar de musica
        {
            DJHand.clip = GetMusicClip(music);
            DJHand.loop = true;
            DJHand.Play();
            Debug.Log("Play: " + music);
        }
        
    }

    private static AudioClip GetMusicClip(Music music)
    {
        foreach (GameAssets.MusicAudioClip musicAudioClip in GameAssets.i.musicAudioClipArray)
        {
            if (musicAudioClip.music == music)
            {
                return musicAudioClip.audioClip;
            }
        }
        Debug.LogError("music " + music + " not found");
        return null;
    }

    private static GameObject ContratarDJ(int sueldo)
    {
        Debug.Log("Contratando DJ...");
        sueldo = 0; // Fumarse el sueldo
        GameObject newDJ = new GameObject("DJ");
        newDJ.AddComponent<DJBrain>();
        // Anidar a LevelPack
        GameObject levelPack = GameObject.FindGameObjectWithTag("LevelPack");
        newDJ.transform.SetParent(levelPack.transform);
        return newDJ;
    }


}
 