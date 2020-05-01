using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GM;
using NaughtyAttributes;
using MoreMountains.NiceVibrations;

public class GameManager : MonoBehaviour
{
    [BoxGroup("GameState")] public GameState state;

    [BoxGroup("Player Name")] public string playerName = "Player";
    [BoxGroup("Bot Names")] public string[] botNames;

    [HideInInspector] public bool clickable;

    public enum GameState
    {
        Win,
        Lose,
        Play,
        Menu
    }

    private void Awake()
    {
        GlobalManager.GameManager = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        clickable = false;
        ChangeState("Menu");
        GlobalManager.UI_Manager.SwitchPanel("Menu");
        GlobalManager.Player.SpawnPlayer();
        //GlobalManager.LevelManager.SpawnLevel();
    }

    public void ChangeState(string _state)
    {
        switch (_state)
        {
            case "Menu":
                state = GameState.Menu;               
                break;
            case "Play":
                state = GameState.Play;
                break;
            case "Win":
                state = GameState.Win;
                break;
            case "Lost":
                state = GameState.Lose;
                break;
            default:
                state = GameState.Menu;
                break;
        }
    }

    public void StartLevel()
    {
        ChangeState("Play");
        GlobalManager.UI_Manager.SwitchPanel("Play");
        GlobalManager.LevelManager.SpawnLevel();
        GlobalManager.UI_Manager.SetLevel(GlobalManager.SaveData.achievedLevel);
        GlobalManager.Player.SpawnPlayer();
        clickable = true;
    }

    public void Win()
    {
        GlobalManager.SaveData.IncrementAchievedLevel();
        GlobalManager.SaveData.IncrementLevel();
        GlobalManager.UI_Manager.SwitchPanel("Win");
        ChangeState("Win");
        clickable = false;
    }

    public void Lose()
    {
        ChangeState("Lose");
        GlobalManager.UI_Manager.SwitchPanel("Lose");
        GlobalManager.Player.KillPlayer();
        clickable = false;
    }

    public void ToMainMenu()
    {
        ChangeState("Menu");
        GlobalManager.UI_Manager.SwitchPanel("Menu");
        clickable = false;
    }
}
