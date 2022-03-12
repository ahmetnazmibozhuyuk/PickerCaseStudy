using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //TODO: Oyun kapanırken vs skor kalınan bölüm gibi özellikleri PlayerPrefs.SetInt gibi bir şekilde kaydet; açılırken PlayerPrefs.GetInt ile al.
    public GameState CurrentState { get; private set; }
    //public Player Player;
    private void Start()
    {
        ChangeState(GameState.GameAwaitingStart);

    }

    public void ChangeState(GameState newState)
    {
        if (CurrentState == newState) return;

        CurrentState = newState;
        switch (newState)
        {
            case GameState.GameAwaitingStart:
                Debug.Log("game is awaiting start");
                break;
            case GameState.GameStarted:
                Debug.Log("game is started");
                break;
            case GameState.GameWon:
                Debug.Log("game is won");
                break;
            case GameState.GameWonResult:
                Debug.Log("game win results");
                break;
            case GameState.GameLost:
                Debug.Log("game is lost");
                break;
            default:
                Debug.LogError("STATE NOT FOUND");
                break;
        }
    }
}
public enum GameState
{
    GamePreStart = 0,
    GameAwaitingStart = 1,
    GameStarted = 2,
    GameWon = 3,
    GameWonResult = 4,
    GameLost = 5,
}
