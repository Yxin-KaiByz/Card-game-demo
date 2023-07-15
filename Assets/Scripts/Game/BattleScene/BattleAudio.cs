using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BattleAudio : MonoBehaviour
{
    private AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        refreshMusic();
        refreshEffect();
        print(music.isPlaying);


    }



    public void refreshMusic()
    {
        GameDataManager gm = GameDataManager.Instance;
        music.volume = gm.musicData.bgValue;
        music.mute = !gm.musicData.isOpenBG;
        music.loop = true;
        music.Play();
    }

    public void refreshEffect()
    {

    }

    public void changeMusic(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("SFX/" + name);
        music.clip = clip;
        refreshMusic();
    
    }
}
