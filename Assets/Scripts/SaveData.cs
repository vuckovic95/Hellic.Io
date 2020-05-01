using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using NaughtyAttributes;

public class SaveData : MonoBehaviour
{
    [BoxGroup("Data")] public int level = 1;
    [BoxGroup("Data")] public int achievedLevel = 1;
    [BoxGroup("Data")] public int score = 0;
    [BoxGroup("Data")] public int gems = 0;

    private void Awake()
    {
        GlobalManager.SaveData = this;
        achievedLevel = PlayerPrefs.GetInt("AchievedLevel", 1);
        level = PlayerPrefs.GetInt("Level", 1);
    }

    #region Save data
    public void SaveLevel()
    {
        PlayerPrefs.SetInt("Level", level);
    }

    public void SaveAchievedLevel()
    {
        PlayerPrefs.SetInt("AchievedLevel", achievedLevel);
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt("Score", score);
    }
    #endregion

    #region Load data
    public void LoadLevel()
    {
        PlayerPrefs.GetInt("Level", level);
    }

    public void LoadAchievedLevel()
    {
        PlayerPrefs.GetInt("AchievedLevel", achievedLevel);
    }

    public void LoadScore()
    {
        PlayerPrefs.GetInt("Score", score);
    }
    #endregion

    public void IncrementLevel()
    {
        level++;
        SaveLevel();
    }

    public void IncrementAchievedLevel()
    {
        achievedLevel++;
        SaveAchievedLevel();
    }

    public void ResetLevels()
    {
        level = 1;
        SaveLevel();
    }
}
