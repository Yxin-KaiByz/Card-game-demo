using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BeginPanel : BasePanel<BeginPanel>
{


    /*public Button beginButton;
    public Button endButton;
    public Button libraryButton;
    public Button settingButton;*/
    // Start is called before the first frame update
    void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void beginTheGame()
    {
        SoundManager.Instance.PlayButtonClick();
        SceneManager.LoadScene("Main");
    }

    public void cardLibrary()
    {
        SoundManager.Instance.PlayButtonClick();
    }

    public void settingList()
    {
        SoundManager.Instance.PlayButtonClick();
        SettingSlider.Instance.showMe();
        hideMe();
    }

    public void quitGame()
    {
        SoundManager.Instance.PlayButtonClick();
        Application.Quit();
    }
}
