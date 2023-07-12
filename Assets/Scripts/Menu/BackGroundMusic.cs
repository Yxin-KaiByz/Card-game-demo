using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundMusic : MonoBehaviour
{
    private static BackGroundMusic instance;
    public static BackGroundMusic Instance => instance;
    private AudioSource audioSource;
    //private bool musicState;
    

    void Start()
    {
        
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
}
