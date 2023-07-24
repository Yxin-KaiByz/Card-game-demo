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

        // ��������
        currentTrans.EndTrans();

        // �ȴ�һ֡
        // ��Ϊ�ҷ�������ڿ�ʼ�����󲻵ȴ�һ֡�Ļ����ڶ���������ʵ��û��ʼ���ţ�
        // �����⶯����ɼ��ľ��ǵ�һ�����������𲻵����ڶ������������á�
        yield return null;

        // �ȴ������������
        while (!currentTrans.isAnimationDone())
            yield return null;
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
    // Update is called once per frame
    void Update()
    {
    }
}
