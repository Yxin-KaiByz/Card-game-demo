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
        StartCoroutine(enteringScene());
    }

    private void Awake()
    {
        instance = this;
        currentTrans.gameObject.SetActive(true);
    }

    private IEnumerator enteringScene()
    {
        currentTrans.EndTrans();
        while (!currentTrans.isAnimationDone())
        {
            yield return null;
        }
        disactiveAllTrans();
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
        while (loading.progress != 1)
            yield return null;

        // 结束过场
        currentTrans.EndTrans();

        // 等待一帧
        // 因为我发现如果在开始动画后不等待一帧的话，第二个动画其实还没开始播放，
        // 后面检测动画完成检测的就是第一个动画，就起不到检测第二个动画的作用。
        yield return null;

        // 等待动画播放完成
        while (!currentTrans.isAnimationDone())
            yield return null;
    }

    private void disactiveAllTrans()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void LoadScene(string sceneName)
    {
        currentTrans.gameObject.SetActive(true);
        StartCoroutine(LoadLevel(sceneName));
    }

    public void setTransition(int transNo)
    {
        transform.GetChild(transNo).gameObject.SetActive(true);
        Transition newTrans = transform.GetChild(transNo).gameObject.GetComponent<Transition>();
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
