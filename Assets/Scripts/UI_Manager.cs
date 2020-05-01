using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using System;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class UI_Manager : MonoBehaviour
{
    #region Properties

    [BoxGroup("UI")] public Transform UI;
    [BoxGroup("UI_Panels")] public GameObject mainMenu;
    [BoxGroup("UI_Panels")] public GameObject playPanel;
    [BoxGroup("UI_Panels")] public GameObject winPanel;
    [BoxGroup("UI_Panels")] public GameObject lostPanel;

    [BoxGroup("LevelProgressSlider")] public Slider slider;
    [BoxGroup("LevelProgressSlider")] public TextMeshProUGUI currentLvl;
    [BoxGroup("LevelProgressSlider")] public TextMeshProUGUI nextLvl;
    [BoxGroup("LevelProgressSlider")] public float slideTime;
  
    [BoxGroup("Sound")] public Image soundImage;

    private Coroutines coroutines;
    private Animator anim;
    private List<GameObject> panels;

    #endregion

    #region Awake/Start/Update

    private void Awake()
    {
        AwakeInit();
    }

    void Start()
    {

    }

    #endregion

    #region Private/Public Functions

    private void AwakeInit()
    {
        GlobalManager.UI_Manager = this;
        anim = GetComponent<Animator>();
        coroutines = GetComponent<Coroutines>();
        FillPanelList();
    }

    private void FillPanelList()
    {
        panels = new List<GameObject>();
        foreach(Transform t in UI)
        {
            panels.Add(t.gameObject);
        }
    }

    public void SwitchPanel(string panelName)
    {
        foreach (GameObject p in panels)
        {
            p.SetActive(false);
        }

        switch (panelName)
        {
            case "Menu":
                mainMenu.SetActive(true);
                break;
            case "Play":
                playPanel.SetActive(true);              
                GlobalManager.GameManager.ChangeState("Play");
                break;
            case "Win":
                winPanel.SetActive(true);
                break;
            case "Lose":
                lostPanel.SetActive(true);
                break;
            default:
                mainMenu.SetActive(true);
                break;
        }
    }
    #endregion

    #region Slider

    //set current slider value, for in-game progress
    public void SetSliderValue(float _value)
    {
        StartCoroutine(coroutines.LerpSlider(slider, slider.value, _value, slideTime));
    }

    //set min and max value of slider, for new lvl for example
    public void SetSlider(float _min, float _max)
    {
        slider.minValue = _min;
        slider.maxValue = _max;
        slider.value = 0f;
    }

    //set current and next level
    public void SetLevel(int _currentLevel)
    {
        currentLvl.text = GlobalManager.SaveData.achievedLevel.ToString();
        //nextLvl.text = (GlobalManager.SaveData.achievedLevel + 1).ToString();
        //ResetSlider();
    }

    //reset slider value
    public void ResetSlider()
    {
        slider.value = 0f;
    }

    public void NextLevel(int _level, float _min, float _max)
    {
        SetLevel(_level);
        SetSlider(_min, _max);
    }
    public void UpdateSoundImage(bool value)
    {
        if (value)
        {
            soundImage.color = Color.white;
        }
        else
        {
            soundImage.color = Color.grey;
        }
    }
    #endregion
}
