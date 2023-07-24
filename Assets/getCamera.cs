using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class getCamera : MonoBehaviour
{
    public static getCamera instance { get; private set; }
    private Canvas canvas;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        canvas = GetComponent<Canvas>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bindMainCamera();
    }

    public void bindMainCamera()
    {
        canvas.worldCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if(canvas.worldCamera == null)
        {
            bindMainCamera ();
        }
    }
}
