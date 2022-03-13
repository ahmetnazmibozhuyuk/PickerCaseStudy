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
                GameAwaitingStartState();
                break;
            case GameState.GameStarted:
                Debug.Log("game is started");
                break;
            case GameState.GameCheckingResults:
                Debug.Log("game checking results");
                //StartCoroutine(Co_NextLevel());
                break;
            case GameState.GameWon:
                Debug.Log("game is won");
                break;
            case GameState.GameLost:
                GameLostState();
                break;
            default:
                Debug.LogError("STATE NOT FOUND");
                break;
        }
    }
    private IEnumerator Co_NextLevel()
    {
        yield return new WaitForSeconds(2);
        ChangeState(GameState.GameStarted);

    }
    private void GameLostState()
    {
        LevelManager.Instance.RestartLevel();
        ChangeState(GameState.GameAwaitingStart);
    }
    private void GameAwaitingStartState()
    {

        LevelManager.Instance.LoadLevels();
    }
}
public enum GameState
{
    GamePreStart = 0,
    GameAwaitingStart = 1,
    GameStarted = 2,
    GameCheckingResults = 3,
    GameWon = 4,
    GameLost = 5,
}
