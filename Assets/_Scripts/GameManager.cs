using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState CurrentState { get; private set; }

    private AudioSource _audioSource;
    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }
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
                GameAwaitingStartState();
                break;
            case GameState.GameStarted:
                GameStartedState();
                break;
            case GameState.GameCheckingResults:
                GameCheckingResultsState();
                break;
            case GameState.GameWon:
                GameWonState();
                break;
            case GameState.GameLost:
                GameLostState();
                break;
            default:
                Debug.LogError("STATE NOT FOUND");
                break;
        }
    }

    private void GameAwaitingStartState()
    {
        Debug.Log("game is awaiting start");
        LevelManager.Instance.LoadLevels();
    }
    private void GameStartedState()
    {
        Debug.Log("game is started");
    }
    private void GameCheckingResultsState()
    {
        Debug.Log("game checking results");
    }
    private void GameWonState()
    {
        Debug.Log("game is won");
    }
    private void GameLostState()
    {
        LevelManager.Instance.RestartLevel();
        ChangeState(GameState.GameAwaitingStart);
    }
    public void PlaySound()
    {
        _audioSource.Play();
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
