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
        if (SceneLoader.Instance.isTransitionDone() && !SoundManager.Instance.isAudioPlaying(0))
        {
            SoundManager.Instance.PlayBgTheme();
        }
    }

    public void beginTheGame()
    {
        SoundManager.Instance.PlayButtonClick();
        SlidingMenu.Instance.SlideLeft();
    }

    public void cardLibrary()
    {

        SoundManager.Instance.PlayButtonClick();
        SceneLoader.Instance.setTransition(1);
        SceneLoader.Instance.LoadScene("CardLibrary");
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
