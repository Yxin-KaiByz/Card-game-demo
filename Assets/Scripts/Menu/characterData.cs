using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterData : MonoBehaviour
{
    public int characterID { get; set; }
    public static characterData Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
