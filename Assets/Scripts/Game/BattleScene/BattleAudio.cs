using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleAudio : MonoBehaviour
{
    private AudioSource music;
    private AudioSource effect;
    GameDataManager gm;
    private static  BattleAudio instance;
    public static BattleAudio Instance => instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        music = GetComponent<AudioSource>();
        refreshMusic();
        refreshEffect();
        print(music.isPlaying);


    }



    public void refreshMusic()
    {
        gm = GameDataManager.Instance;
        music.volume = gm.musicData.bgValue;
        music.mute = !gm.musicData.isOpenBG;
        music.loop = true;
        music.Play();
    }

    public void refreshEffect()
    {
        effect = this.AddComponent<AudioSource>();
        effect.volume = gm.musicData.effectValue;
        effect.mute = !gm.musicData.isOpenEffect;
        effect.loop = false;
        
    }

    public void changeMusic(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("SFX/" + name);
        music.clip = clip;
        refreshMusic();
    
    }

    public void changeEffect(string name)
    {
        print("In battle audio");
        AudioClip clip = Resources.Load<AudioClip>("SFX/" + name);
        effect.clip = clip;
        effect.Play();

    }


}
