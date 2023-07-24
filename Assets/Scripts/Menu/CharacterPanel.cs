using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : BasePanel<CharacterPanel>
{
    private Image charBackground;
    private GameObject charInfo;

    public void Awake()
    {
        charBackground = GetComponent<Image>();
        charInfo = transform.GetChild(0).gameObject;
    }

    private void loadBgSprite(string spritePath)
    {
        Sprite bg = Resources.Load<Sprite>(spritePath);
        charBackground.sprite = bg;
    }
    private void changeBgColor(float tpLevel, float r = 1f, float g = 1f, float b =1f)
    {
        var color = charBackground.color;
        color.a = tpLevel;
        color.r = r; 
        color.g = g;
        color.b = b;
        charBackground.color = color;
    }

    private void setCharInfo(string name, Color color)
    {

        Text charText = charInfo.GetComponent<Text>();
        charText.text = name;
        charText.color = color;
    }
    public void backToMenu()
    {
        charInfo.gameObject.SetActive(false);
        SlidingMenu.Instance.SlideRight();
        SoundManager.Instance.PlayButtonClick();
        loadBgSprite("unity_builtin_extra/Background");
        changeBgColor(0.8f, 0.2f,0.2f,0.2f);
    }

    public void startGame()
    {
        SceneLoader.instance.setTransition(1);
        SoundManager.Instance.PlayButtonClick();
        SceneLoader.instance.LoadScene("Main");
    }
    public void characterOne()
    {
        characterData.Instance.characterID = 0;
        setCharInfo("Bronya", new Color(0f,0.2f, 1f));
        charInfo.gameObject.SetActive(true);
        loadBgSprite("Image/Character/charBackground_Bronya");
        changeBgColor(1.0f);
    }

    public void characterTwo()
    {
        characterData.Instance.characterID = 1;
        setCharInfo("Elysia", new Color(0.8f, 0.3f, 0.8f));
        charInfo.gameObject.SetActive(true);
        loadBgSprite("Image/Character/charBackground_Elysia");
        changeBgColor(1.0f);
    }

    public void characterThree()
    {
        setCharInfo("Coming Soon", Color.white);
        charInfo.gameObject.SetActive(false);
        loadBgSprite("unity_builtin_extra/Background");
        changeBgColor(0.8f, 0.2f, 0.2f, 0.2f);
    }
}
