using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel<T> : MonoBehaviour where T:class
{
    private static T instance;

    public static T Instance => instance;
    // Start is called before the first frame update

    private void Awake()
    {

        instance = this as T;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void showMe()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void hideMe()
    {
        this.gameObject.SetActive(false);
    }
}
