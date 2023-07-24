using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public Transition currentTrans;
    private Canvas canvas;
    private bool isTitlePlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        bindMainCamera();
        currentTrans.gameObject.SetActive(true);
        StartCoroutine(enteringScene());
        if(!isTitlePlayed ) { 
            SoundManager.Instance.playTitle();
            isTitlePlayed = true;
        }
        
    }

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
        Debug.Log("startTrans--Start");
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
        Debug.Log("startTrans--Done");
        loading.allowSceneActivation = true;
        while (loading.progress != 1)
            yield return null;
        Debug.Log("endTrans--Start");
        // ��������
        currentTrans.EndTrans();

        // �ȴ�һ֡
        // ��Ϊ�ҷ�������ڿ�ʼ�����󲻵ȴ�һ֡�Ļ����ڶ���������ʵ��û��ʼ���ţ�
        // �����⶯����ɼ��ľ��ǵ�һ�����������𲻵����ڶ������������á�
        yield return null;

        // �ȴ������������
        while (!currentTrans.isAnimationDone())
            yield return null;
        Debug.Log("endTrans--Done");
        disactiveAllTrans();
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
    public void bindMainCamera()
    {
        canvas.worldCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if (canvas.worldCamera == null)
        {
            bindMainCamera();
        }
    }
}
