using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance => instance;
    private AudioSource audioSource;
    private AudioSource buttonEffect;
    //private bool musicState;
    private AudioClip hoverClip;
    private AudioClip clickClip;

    void Start()
    {
        buttonEffect = GetComponent<AudioSource>();
        hoverClip = Resources.Load<AudioClip>("SFX/Button_Hover");
        clickClip = Resources.Load<AudioClip>("SFX/Button_Click");
    }

    // Start is called before the first frame update
    void Awake()
    {

        instance = this;
        audioSource = this.GetComponent<AudioSource>();
        //musicState = true;
    }

    public void ChangeValue(float value)
    {
        audioSource.volume = value;
    }

    public void ChangeOpen(bool isOpen)
    {
        //musicState = !musicState;
        //¿ªÆô´ú±ímute
        audioSource.mute = !isOpen;
    }

    public void PlayButtonHover()
    {
        buttonEffect.PlayOneShot(hoverClip);
    }
    public void PlayButtonClick()
    {
        buttonEffect.PlayOneShot(clickClip);
    }
}
