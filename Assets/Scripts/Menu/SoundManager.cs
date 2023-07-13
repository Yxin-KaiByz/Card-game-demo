using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance => instance;
    private AudioSource[] audioLs;
    private AudioSource bgSource;
    private AudioSource buttonEffect;
    //private bool musicState;
    private AudioClip hoverClip;
    private AudioClip clickClip;

    void Start()
    {
        
    }

    // Start is called before the first frame update
    void Awake()
    {

        instance = this;
        audioLs = GetComponents<AudioSource>();
        bgSource = audioLs[0];
        buttonEffect = audioLs[1];
        hoverClip = Resources.Load<AudioClip>("SFX/Button_Hover");
        clickClip = Resources.Load<AudioClip>("SFX/Button_Click");
        SettingSlider.Instance.updatePanelInfor();
        //musicState = true;
    }

    public void ChangeBgValue(float value)
    {
        bgSource.volume = value;
    }

    public void ChangeBgOpen(bool isOpen)
    {
        //musicState = !musicState;
        //开启代表mute
        bgSource.mute = !isOpen;
    }

    public void ChangeEtValue(float value)
    {
        buttonEffect.volume = value;
    }

    public void ChangeEtOpen(bool isOpen)
    {
        //musicState = !musicState;
        //开启代表mute
        buttonEffect.mute = !isOpen;
    }

    public void PlayBgTheme()
    {
        bgSource.loop = true;
        bgSource.Play();
    }
    public void PlayButtonHover()
    {
        buttonEffect.PlayOneShot(hoverClip);
    }
    public void PlayButtonClick()
    {
        buttonEffect.PlayOneShot(clickClip);
    }

    public bool isAudioPlaying(int i)
    {
        return audioLs[i].isPlaying;
    }
}
