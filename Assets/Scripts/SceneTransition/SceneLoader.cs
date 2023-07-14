using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    public Transition currentTrans;
    public static SceneLoader Instance => instance;
    // Start is called before the first frame update
    void Start()
    {
        currentTrans.EndTrans();
    }

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator LoadLevel(string sceneName)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
        loading.allowSceneActivation = false;
        //play start transition
        currentTrans.StartTrans();
        //Wait for 1 frame
        yield return null;
        //Wait for scene loading is almost finish
        while (loading.progress < 0.899)
        {
            yield return null;
        }
        while (!currentTrans.isAnimationDone())
        {
            yield return null;
        }
        loading.allowSceneActivation = true;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    public void setTransition(int i)
    {
        Transition newTrans = GetComponentsInChildren<Transition>()[i];
        currentTrans = newTrans;
    }
    
    public bool isTransitionDone()
    {
        return currentTrans.isAnimationDone();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
